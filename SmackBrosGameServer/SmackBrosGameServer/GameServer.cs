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
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace SmackBrosGameServer
{
    public partial class GameServer : GameWindow
    {
        private static bool _debugMode = true;
        private GameData _gameMetadata;
        public static DateTime? LastNetworkReceived = null;
        static int _port1 = 1521;
        static int _port2 = 1522;
        static int _port3 = 1523;

        private int _numSmackers;

        private string _serverIp;
        const bool _isServer = true;
        List<string> _clientIpList; 
        Thread _receivingThread;
        Thread _receiveStatesThread;
        UdpClient _client;
        UdpClient _client2;
        UdpClient _client3;
        readonly object _packetProcessQueueLock = new object();
        Queue<Packet> _packetProcessQueue = new Queue<Packet>();

        List<InputPacket> _clientInputBuffer = new List<InputPacket>();
        List<Smacker> _smackerList = new List<Smacker>();
        List<Tuple<int, Input>> _playerInputs = new List<Tuple<int, Input>>();

        bool _acceptingNewClients;
        public bool ServerInitialized = false;

        public GameObjectRegister ObjectRegister = new GameObjectRegister();

        public GameServer()
        {
            _acceptingNewClients = true;
            _gameMetadata = new GameData()
            {
                GamePaused = false,
                PauseAlpha = 0,
                GameReadyToStart = false,
                FrameNumber = null
            };
        }
        private void Update(GameTime gameTime)
        {
            _gameMetadata.PauseAlpha += gameTime.ElapsedGameTime.TotalMilliseconds;
            lock (_packetProcessQueueLock)
                while (_packetProcessQueue.Any())
                {
                    var packet = _packetProcessQueue.Dequeue();
                    if (packet.GetPacketId() == 1)
                    {
                        var packet2 = (QueueInteractionPacket)packet;
                        
                    }
                    if (packet.GetPacketId() == 2)
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
                    if (packet.GetPacketId() == 3)
                    {
                        var packet2 = (QueueFinishedPacket)packet;
                        new Task(() =>
                        {

                            //var response = Interaction.MsgBox("Would you like to allow " + packet2.nameToConnect + " to connect?", MsgBoxStyle.YesNo);
                            if (_acceptingNewClients)
                            {
                                _clientIpList.Add(packet2.IpAddressToConnectTo);
                                PacketQueue.Instance.AddPacket(new GameServerAcceptedJoinPacket { Accepted = true, IpToSend = packet2.IpAddressToConnectTo });
                                SendStatePacket(true);
                            }
                            else
                            {
                                PacketQueue.Instance.AddPacket(new GameServerAcceptedJoinPacket { Accepted = false, IpToSend = packet2.IpAddressToConnectTo });
                            }
                        }).Start();
                    }
                    /*if (packet.GetPacketID() == 7)
                    {
                        var packet2 = (GameStatePacket)packet;
                        UpdateToState(packet2);
                        FrameNumber = 0;
                    }*/
                    if (packet.GetPacketId() == 8)
                    {
                        var packet2 = (InputPacket)packet;
                        if (!_gameMetadata.FrameNumber.HasValue)
                            //x frame delay before starting inputs. This allows the other players inputs to be here before we start processing.
                            _gameMetadata.FrameNumber = 0;
                        _clientInputBuffer.Add(packet2);
                    }
                    if (packet.GetPacketId() == 9)
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
                Down = input.Down,
                Up = input.Up,
                Left = input.Left,
                Right = input.Right,
                DownC = input.DownC,
                UpC = input.UpC,
                RightC = input.RightC,
                LeftC = input.LeftC,
                RightTrigger = input.RightTrigger,
                LeftTrigger = input.LeftTrigger,
            };
            _playerInputs.Add(new Tuple<int, Input>(input.PlayerNum, playerInput));
        }
        private void KeyDown(object sender, EventArgs args)
        {
 
        }
        private void SendStatePacket(bool guaranteed = false)
        {
            var pack = new GameStatePacket()
            {
                Smackers = _smackerList.Select(x => new PickledSmackerData(x.Position, x.SmackerId, (short)x.State, (short)x.FrameDurationCurState, x.IsFacingRight)).ToList()
            };
            var dat = new List<byte>();
            pack.WritePacketData(dat);
            if (guaranteed)
            {
                PacketQueue.Instance.AddPacket(pack);
            }
            else
            {
                foreach (string clientIp in _clientIpList)
                {
                    _client3.Send(dat.ToArray(), dat.Count,
                        new IPEndPoint(new IPAddress(clientIp.Split('.').Select(byte.Parse).ToArray()), _port3));
                }
            }
        }
        void StartThreads()
        {
            new Task(() =>
            {
                _client = _client ?? new UdpClient(_port1, AddressFamily.InterNetwork);
                _client2 = _client2 ?? new UdpClient(_port2, AddressFamily.InterNetwork);
                _client3 = _client3 ?? new UdpClient(_port3, AddressFamily.InterNetwork);
                IPHostEntry host;
                string localIp = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIp = ip.ToString();
                    }
                }
                _serverIp = localIp;
                _receivingThread =
                    new Thread(
                        () =>
                            PacketQueue.Instance.ReceivingLoop(_client2, new IPEndPoint(IPAddress.Any, _port2),
                                _packetProcessQueue, _packetProcessQueueLock))
                    {
                        IsBackground = true
                    };
                _receivingThread.Start();
            }).Start();
            ServerInitialized = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            int i = 0;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color4.Purple);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }


    }
}

