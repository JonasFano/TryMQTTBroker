using Software_measure.Model;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

// Check port:
// netstat -an | find "1883"


public static class MqttClientManager
{
    public static void Main(string[] args)
    {
        Task<bool> isConnected = ConnectAsyncToBroker();
        Console.ReadLine();
    }

    private static MqttClient? mqttClient;

    private static string brokerAddress = "localhost"; // "tcp://172.31.1.20"; //"tcp://127.0.0.1"; //"tcp://172.31.1.20";
    private static int brokerPort = 1883;

    // clientid mit dem Präfix "user-" und der eindeutigen GUID generieren
    private static string clientId = "user-" + Guid.NewGuid().ToString();

    public static async Task<bool> ConnectAsyncToBroker()
    {
        try
        {
            mqttClient = new MqttClient(brokerAddress, brokerPort, false, null, null, MqttSslProtocols.None);

            // Event-Handler für Nachrichtenregistrierung und Verbindungsbrechung hinzufügen
            mqttClient.MqttMsgPublishReceived += HandleReceivedMessage;
            mqttClient.ConnectionClosed += HandleConnectionClosed;

            // Verbindungsaufbau asynchron durchführen
            await Task.Run(() =>
            {
                byte result = mqttClient.Connect(clientId);
                if (result == MqttMsgConnack.CONN_ACCEPTED)
                {
                    // Verbindung erfolgreich
                    // Topics abonnieren, von denen Sie Daten erhalten möchten
                    SubscribeToTopics();
                }
            });

            return true; // Erfolgreich verbunden
        }

        catch (Exception ex)
        {
            // Fehler beim Verbinden
            Console.WriteLine("Fehler beim Verbinden zum Broker: " + ex.Message);
        }

        return false;
    }

    private static void SubscribeToTopics()
    {
        // Topics, von denen Sie Daten erhalten möchten, hier abonnieren
        string[] topics = new string[]
        {
        "SunriseRC/Current/Text/CartesianFlange",
            // Hier weitere Topics ergänzen, falls erforderlich
        };

        // QoS-Level für die Abonnements festlegen
        byte[] qosLevels = new byte[topics.Length];
        for (int i = 0; i < qosLevels.Length; i++)
        {
            qosLevels[i] = MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE;
        }

        // Topics abonnieren
        mqttClient.Subscribe(topics, qosLevels);
    }


    private static void HandleReceivedMessage(object sender, MqttMsgPublishEventArgs e)
    {
        try
        {
            // Nachricht in UTF-8 formatieren
            string message = System.Text.Encoding.UTF8.GetString(e.Message);

            // Nachricht verarbeiten - Hier können Sie den Code einfügen, um die Koordinaten zu extrahieren und zu verwenden
            // Beispiel: 
            points_3D point = ExtractCoordinates(message);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Verarbeiten der empfangenen Nachricht: " + ex.Message);
        }
    }

    private static void HandleConnectionClosed(object sender, EventArgs e)
    {
        // Hier können Sie den Code hinzufügen, um auf eine Verbindungsunterbrechung zu reagieren
        Console.WriteLine("Verbindung zum Broker wurde unterbrochen.");
    }

    // Beispiel für das Extrahieren der Koordinaten aus der empfangenen Nachricht
    public static points_3D ExtractCoordinates(string message)
    {
        points_3D point = new points_3D();
        try // "0.0", "0.0", "0.0"
        {
            string[] parts = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                string[] keyValue = part.Split('=');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    string value = keyValue[1];

                    // X-Koordinate
                    if (key == "X")
                    {
                        point.X = double.Parse(value);
                    }

                    // Y-Koordinate
                    if (key == "Y")
                    {
                        point.Y = double.Parse(value);
                    }

                    // Z-Koordinate
                    if (key == "Z")
                    {
                        point.Z = double.Parse(value);
                    }
                }
            }
            return point;
        }
        catch
        {
            return point;
        }
    }
}
