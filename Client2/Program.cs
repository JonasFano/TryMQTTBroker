using MQTTnet;
using MQTTnet.Client;


class Client
{
    private IMqttClient? mqttClient;

    public Client()
    {
        var mqttFactory = new MqttFactory();
        mqttClient = mqttFactory.CreateMqttClient();

        Connect();
    }

    public void Connect()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();

        while (true)
        {
            try
            {
                var response = mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                response.Wait();
                Console.WriteLine("The MQTT client is connected.");
                Console.WriteLine(response.Result.ResultCode.ToString());
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("connection failed");
            }
            Task.Delay(100).Wait();
        }
    }

    public void Close()
    {
        var res = mqttClient.DisconnectAsync();
        res.Wait();
    }

    public void SendMSG(string msg)
    {
        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic("SunriseRC/Current/Text/CartesianFlange")
            .WithPayload(msg)
            .Build();
        try
        {
            var res = mqttClient?.PublishAsync(applicationMessage, CancellationToken.None);
            res?.Wait();
            Console.WriteLine(res?.Result.IsSuccess);
        }
        catch (Exception ex)
        {
            Connect();
        }
    }

    public static void Main(string[] args)
    {
        Client client = new Client();
        var rnd = new Random();
        while (true) {
            client.SendMSG("X=" + (rnd.NextDouble() * 100 - 50) + " " + "Y=" + (rnd.NextDouble() * 100 - 50) + " " + "Z=" + (rnd.NextDouble() * 100 - 50));
            Task.Delay(1000).Wait();
        }

        client.Close();
    }
}