using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using SharpGL.SceneGraph;
using SharpGL;

namespace SmackBrosGameServer
{
    public partial class GameServerWindow : Window
    {
        private static bool DebugMode = true;
        private GameData GameMetadata;
        public static DateTime? lastNetworkReceived = null;
        static int port1 = 1521;
        static int port2 = 1522;
        static int port3 = 1523;

        private int numSmackers;
        
        bool? IsServer;
        string ServerIP;
        List<string> ClientIPList; 
        Thread ReceivingThread;
        Thread ReceiveStatesThread;
        UdpClient client;
        UdpClient client2;
        UdpClient client3;
        readonly object packetProcessQueueLock = new object();
        Queue<Packet> packetProcessQueue = new Queue<Packet>();

        List<InputPacket> ClientInputBuffer = new List<InputPacket>();
        readonly object ServerStateBufferLock = new object();
        List<GameStatePacket> ServerStateBuffer = new List<GameStatePacket>();

        List<Smacker> smackerList = new List<Smacker>();
        List<Tuple<int, Input>> playerInputs = new List<Tuple<int, Input>>();

        bool acceptingNewClients;
        public bool serverInitialized = false;

        public GameServerWindow()
        {
            InitializeComponent();
            acceptingNewClients = true;
            GameMetadata = new GameData()
            {
                GamePaused = false,
                pauseAlpha = 0,
                GameReadyToStart = false,
                FrameNumber = null
            };
        }
        private void Update(GameTime gameTime)
        {
            GameMetadata.pauseAlpha += gameTime.ElapsedGameTime.TotalMilliseconds;
            lock (packetProcessQueueLock)
                while (packetProcessQueue.Any())
                {
                    var packet = packetProcessQueue.Dequeue();
                    if (packet.GetPacketID() == 1)
                    {
                        var packet2 = (QueueInteractionPacket)packet;
                        
                    }
                    if (packet.GetPacketID() == 2)
                    {
                        var packet2 = (QueueStatusUpdatePacket)packet;
                        Console.WriteLine(packet2.Accepted);
                        if (!packet2.Accepted)
                        {
                            /*IsServer = null;
                            selectedUnits.Clear();
                            ServerIP = null;
                            ReceivingThread.Abort();
                            ReceivingThread = null;
                            ReceiveStatesThread.Abort();
                            ReceiveStatesThread = null;
                            curPlayer.playerID = false;
                            Interaction.MsgBox(
                                "Server declined your request to connect. You may try connecting to another host.");*/
                        }
                    }
                    if (packet.GetPacketID() == 3)
                    {
                        var packet2 = (QueueFinishedPacket)packet;
                        new Task(() =>
                        {

                            //var response = Interaction.MsgBox("Would you like to allow " + packet2.nameToConnect + " to connect?", MsgBoxStyle.YesNo);
                            if (acceptingNewClients)
                            {
                                ClientIPList.Add(packet2.ipAddressToConnectTo);
                                PacketQueue.Instance.AddPacket(new GameServerAcceptedJoinPacket { Accepted = true, ipToSend = packet2.ipAddressToConnectTo });
                                SendStatePacket(true);
                            }
                            else
                            {
                                PacketQueue.Instance.AddPacket(new GameServerAcceptedJoinPacket { Accepted = false, ipToSend = packet2.ipAddressToConnectTo });
                            }
                        }).Start();
                    }
                    /*if (packet.GetPacketID() == 7)
                    {
                        var packet2 = (GameStatePacket)packet;
                        UpdateToState(packet2);
                        FrameNumber = 0;
                    }*/
                    if (packet.GetPacketID() == 8)
                    {
                        var packet2 = (InputPacket)packet;
                        if (!GameMetadata.FrameNumber.HasValue)
                            //x frame delay before starting inputs. This allows the other players inputs to be here before we start processing.
                            GameMetadata.FrameNumber = 0;
                        ClientInputBuffer.Add(packet2);
                    }
                    if (packet.GetPacketID() == 9)
                    {
                        var packet2 = (QueueGameInfoPacket)packet;
                    }
                }
        } 
        private void UpdateFromClientGamepad(InputPacket input)
        {
            //update position based on client input
            Input playerInput = new Input()
            {
                A = input.A,
                B = input.B,
                X = input.X,
                Y = input.Y,
                RightAnalog = input.RightAnalog,
                LeftAnalog = input.LeftAnalog,
                Start = input.Start,
                down = input.Down,
                up = input.Up,
                left = input.Left,
                right = input.Right,
                downC = input.DownC,
                upC = input.UpC,
                rightC = input.RightC,
                leftC = input.LeftC,
                RightTrigger = input.RightTrigger,
                LeftTrigger = input.LeftTrigger,
            };
            playerInputs.Add(new Tuple<int, Input>(input.playerNum, playerInput));
        }
        private void KeyDown(object sender, OpenGLEventArgs args)
        {
 
        }
        private void SendStatePacket(bool guaranteed = false)
        {
            var pack = new GameStatePacket()
            {
                Smackers = smackerList.Select(x => new PickledSmackerData(x.Position, x.SmackerId, (short)x.State, (short)x.FrameDurationCurState, x.IsFacingRight)).ToList()
            };
            var dat = new List<byte>();
            pack.WritePacketData(dat);
            if (guaranteed)
            {
                PacketQueue.Instance.AddPacket(pack);
            }
            else
            {
                foreach (string ClientIP in ClientIPList)
                {
                    client3.Send(dat.ToArray(), dat.Count,
                        new IPEndPoint(new IPAddress(ClientIP.Split('.').Select(byte.Parse).ToArray()), port3));
                }
            }
        }
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            if (DebugMode)
            {
                //  Get the OpenGL object.
                OpenGL gl = openGLControl.OpenGL;
                //  Clear the color and depth buffer.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                //  Load the identity matrix.
                gl.LoadIdentity();
            }
        }
        void StartThreads()
        {
            new Task(() =>
            {
                client = client ?? new UdpClient(port1, AddressFamily.InterNetwork);
                client2 = client2 ?? new UdpClient(port2, AddressFamily.InterNetwork);
                client3 = client3 ?? new UdpClient(port3, AddressFamily.InterNetwork);
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                    }
                }
                ServerIP = localIP;
                ReceivingThread = new Thread(() => PacketQueue.Instance.ReceivingLoop(client2, new IPEndPoint(IPAddress.Any, port2), packetProcessQueue, packetProcessQueueLock));
                ReceivingThread.IsBackground = true;
                ReceivingThread.Start();
            }).Start();
            serverInitialized = true;
        }
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        private float rotation = 0.0f;
    }
}
