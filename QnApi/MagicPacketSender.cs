using System;
using System.Globalization;
using System.Net.Sockets;
using System.Threading.Tasks;

//we derive our class from a standart one
public class MagicPacketSender : UdpClient
{
  public async Task<int> WakeUp(string macAddress)
  {
    Connect(new System.Net.IPAddress(0xffffffff), 0x2fff); //255.255.255.255 broadcast and port=12287
    SetClientToBrodcastMode();

    byte[] bytes = MacAddressToByteArray(macAddress);

    //now send wake up packet
    return await SendAsync(bytes, 1024);
  }


  //this is needed to send broadcast packet
  private void SetClientToBrodcastMode()
  {
    if (this.Active)
      this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                SocketOptionName.Broadcast, 0);
  }

  private static byte[] MacAddressToByteArray(string macAddress)
  {
    macAddress = macAddress.Replace(":", string.Empty);
    //set sending bites
    int counter = 0;
    //buffer to be send
    byte[] bytes = new byte[1024];   // more than enough :-)
                                     //first 6 bytes should be 0xFF
    for (int y = 0; y < 6; y++)
      bytes[counter++] = 0xFF;
    //now repeate MAC 16 times
    for (int y = 0; y < 16; y++)
    {
      int i = 0;
      for (int z = 0; z < 6; z++)
      {
        bytes[counter++] = byte.Parse(macAddress.Substring(i, 2), NumberStyles.HexNumber);
        i += 2;
      }
    }

    return bytes;
  }
}