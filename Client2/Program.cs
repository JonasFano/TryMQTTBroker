using MQTTnet;
using MQTTnet.Client;


class Client
{
    private IMqttClient? mqttClient;

    public Client()
    {
        var mqttFactory = new MqttFactory();

        mqttClient = mqttFactory.CreateMqttClient();
        // Use builder classes where possible in this project.
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("192.168.2.147").Build();

        var response = mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        response.Wait();

        Console.WriteLine("The MQTT client is connected.");
        Console.WriteLine(response.Result.ResultCode.ToString());
    }

    public void Close()
    {
        var res = mqttClient.DisconnectAsync();
        res.Wait();
    }

    public void SendMSG(string msg)
    {
        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic("samples/temperature/living_room")
            .WithPayload(msg)
            .Build();

        var res = mqttClient?.PublishAsync(applicationMessage, CancellationToken.None);
        res?.Wait();
        Console.WriteLine(res?.Result.IsSuccess);
    }

    public static void Main(string[] args)
    {
        Client client = new Client();
        var rnd = new Random();
        for (int i = 0; i < 100; i++) {
            client.SendMSG("X=" + (rnd.NextDouble() * 100 - 50) + " " + "Y=" + (rnd.NextDouble() * 100 - 50) + " " + "Z=" + (rnd.NextDouble() * 100 - 50));
            Task.Delay(1000).Wait();
        }

        client.Close();
    }
}