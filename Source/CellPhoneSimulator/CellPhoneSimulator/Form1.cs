using System;

using System.Windows.Forms;
using Common.Entities;
using Common.Helpers;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CellPhoneSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {

            //Recharge r = new Recharge { Id = 1284641, PhoneNumber = "3141234567", Value = 10000, Date = DateTime.Now, State = 0 };
            //var json1 = SerializationHelpers.SerializeJson(r);
            //Price p = new Price { Id = 1, Description = "", Prices = 0 };
            //var json = SerializationHelpers.SerializeJson(p);

            Customer cus = new Customer();
            cus.Id = Convert.ToInt32(txtid.Text);
            cus.Id_Type = 1;
            cus.FirstName = txtfName.Text;
            cus.PhoneNumber = txtPhone.Text;
            cus.SecondName = txtsName.Text;
            cus.LastName = txtlName.Text;

            var postString = SerializationHelpers.SerializeJson(cus);
            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);

            HttpWebRequest request;
            request = WebRequest.Create("http://localhost:54371/CallMonitorAPI.svc/Register") as HttpWebRequest;
            request.Timeout = 10 * 1000;
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "text/plain; charset=utf-8";

            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            var DataContractJsonSerializer = new DataContractJsonSerializer(typeof(Customer));
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string body = reader.ReadToEnd();
            var stream = response.GetResponseStream();
            var userinfo = SerializationHelpers.DeserializeJson<Response>(stream);
            txtResposeArea.Text = "Id Response: " + userinfo.response + "\n Response: " + userinfo.response + "\n Exception: " + userinfo.exception.Message;


            //var request = (HttpWebRequest)WebRequest.Create(string.Format("http://localhost:54371/CallMonitorAPI.svc/Register"));
            //var body = "";

            //request.Method = "GET";
            //request.ContentType = @"text/plain; charset=uf-8";

            //var DataContractJsonSerializer = new DataContractJsonSerializer(typeof(Customer));
            //using (var memoryStream = new MemoryStream())
            //using (var reader = new StreamReader(memoryStream))
            //{
            //    DataContractJsonSerializer.WriteObject(memoryStream, cus);
            //    memoryStream.Position = 0;
            //    body = reader.ReadToEnd();
            //}
            //using (var streaWriter = new StreamWriter(request.GetRequestStream()))
            //{
            //    streaWriter.Write(body);
            //}
            //var response = (HttpWebResponse)request.GetResponse();
            //var stream = response.GetResponseStream();
            //var userinfo = (Response)DataContractJsonSerializer.ReadObject(stream);
            //txtResposeArea.Text = "Id Response: " + userinfo.response + "\n Response: " + userinfo.response + "\n Exception: " + userinfo.exception.Message;
        }
    }
}
