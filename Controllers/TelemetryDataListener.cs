using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Codemasters.F1_2020;
using F12020.Backend.Shared;

namespace F12020.Backend.Controllers
{
    public class TelemetryDataListener
    {
        private UdpClient _asd;
        public TelemetryDataListener()
        {
            StartListener();
        }

        public static async Task StartListener()
        {
            Config config = Config.Load();
            IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Parse(config.IPSettings.ListenIPv4), config.IPSettings.ListenPort);
            UdpClient listener = new UdpClient(ipendpoint);
            
            Console.WriteLine($"Starting UDP listener on IP {config.IPSettings.ListenIPv4} Port {config.IPSettings.ListenPort}");
            int counter = 0;
            try
            {
                while (true)
                {
                    counter++;
                    UdpReceiveResult udppacket = await listener.ReceiveAsync();
                    byte[] bytes = udppacket.Buffer;

                    PacketType pt = CodemastersToolkit.GetPacketType(bytes);

                    switch (pt)
                    {
                        case PacketType.Lap:
                            LapPacket lappacket = new LapPacket();
                            lappacket.LoadBytes(bytes);

                            Console.WriteLine($"Laptime: {lappacket.FieldLapData[lappacket.PlayerCarIndex].CurrentLapInvalid}");

                            break;
                        default:
                            break;
                    }


                    //Console.WriteLine($"{counter}: {pt.ToString()}");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}