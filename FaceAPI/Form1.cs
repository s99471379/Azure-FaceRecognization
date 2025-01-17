using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace FaceAPI
{
    public partial class Form1 : Form
    {
        String imagePath = "c:\\temp\\";
        String imageName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                imageName = DateTime.Now.Ticks.ToString();
                textBox2.Text = "";
                saveImage(textBox1.Text);
                pictureBox1.Image = Image.FromFile(imagePath + imageName + ".png");
                MakeRequest(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveImage(String url)
        {
            try
            {
                WebClient webclient1 = new WebClient();
                webclient1.DownloadFile(url, imagePath + imageName + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void MakeRequest(String texturl)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            var uri = "";
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3576fe296f7a4de281dd465ffede5c14");

            String detials = "";
            uri = "https://eastasia.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,emotion&recognitionModel=recognition_04&returnRecognitionModel=false&detectionModel=detection_01&faceIdTimeToLive=86400";

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{\"url\":\"" + texturl + "\"}");
            String respString = "";
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                respString = await response.Content.ReadAsStringAsync();
                textBox2.Text = respString;
                analysisJson(respString);
            }


        }
        private void analysisJson(String jsonString)
        {
            Bitmap bitmap1 = new Bitmap(imagePath + imageName + ".png");
            Double max = 0;
            String feel = "";
            dynamic json = JsonConvert.DeserializeObject(jsonString);
            foreach (var item in json)
            {
                
                foreach(var emotion in item["faceAttributes"]["emotion"])
                {
                    var e = emotion.Name + emotion.Value;
                    if (emotion.Value >= max)
                    {
                        max = emotion.Value.ToObject<double>();
                        feel = emotion.Name;
                    }

                }
                String faceAttributes = item["faceAttributes"]["gender"] + "," + item["faceAttributes"]["age"] + feel;
                bitmap1 = DrawFace(bitmap1, faceAttributes, item);
            }
            bitmap1.Save(imagePath + imageName + "-1.png");
            pictureBox1.Image = Image.FromFile(imagePath + imageName + "-1.png");
        }

        private Bitmap DrawFace(Bitmap bitmap1,String faceAttributes, dynamic face1)
        {
            int l = 0, t = 0, w = 0, h = 0;
            l = face1["faceRectangle"]["left"].ToObject<int>();
            t = face1["faceRectangle"]["top"].ToObject<int>();
            w = face1["faceRectangle"]["width"].ToObject<int>();
            h = face1["faceRectangle"]["height"].ToObject<int>();

            Graphics graphics1 = Graphics.FromImage(bitmap1);
            var pen1 = new Pen(Color.Red, 8);
            graphics1.DrawRectangle(pen1, l, t, w, h);
            graphics1.DrawString(faceAttributes, new Font("Tahoma", 12), Brushes.White, new PointF(l, t - 20));

            return bitmap1;
        }
    }
}