using System;

using System.Windows.Forms;
using Common.Entities;
using Common.Helpers;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Data;

namespace CellPhoneSimulator
{
    public partial class Cellphone_Client : Form
    {
        public const string url = "http://localhost:54371/CallMonitorAPI.svc"; 
        public Cellphone_Client()
        {
            InitializeComponent();
            loadItems();
        }

        private void loadItems()
        {
            txtPriceDate.Text = DateTime.Now.ToString();
            txtReDate.Text = DateTime.Now.ToString();

            DataTable dtID = new DataTable();
            dtID.Columns.Add("Code", typeof(int));
            dtID.Columns.Add("Description", typeof(string));
            dtID.Rows.Add(1, "Cedula");
            dtID.Rows.Add(2, "Another");
            cbtypeId.DataSource = dtID;
            cbtypeId.DisplayMember = "Description";
            cbtypeId.ValueMember = "Code";

            DataTable dtprice = new DataTable();
            dtprice.Columns.Add("Code", typeof(int));
            dtprice.Columns.Add("Description", typeof(string));
            dtprice.Rows.Add(1, "Minute Price");
            dtprice.Rows.Add(2, "Second Price");
            cbPrice.DataSource = dtprice;
            cbPrice.DisplayMember = "Description";
            cbPrice.ValueMember = "Code";
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                Customer cus = new Customer
                {
                    Id = Convert.ToInt32(txtid.Text),
                    Id_Type = Convert.ToInt32(cbtypeId.SelectedValue),
                    FirstName = txtfName.Text,
                    PhoneNumber = txtPhone.Text,
                    SecondName = txtsName.Text,
                    LastName = txtlName.Text
                };
                var json = SerializationHelpers.SerializeJson(cus);
                var resp = ResponseCallService(json, "POST", "Register");
                var userinfo = SerializationHelpers.DeserializeJson<Response>(resp);
                txtResposeArea.Text = "Id Response: " + userinfo.idResponse + "\n Response: " + userinfo.response + "\n Exception: " + ((userinfo.exception != null) ? userinfo.exception.Message : "");
            }
            catch (Exception ex)
            {
                txtResposeArea.Text = "Oops... Something went wrong: \nException: " + ex.Message;
            }
        }

        private void btnRecharge_Click(object sender, EventArgs e)
        {
            try
            {
                Recharge r = new Recharge { Id = Convert.ToInt32(txtReId.Text), PhoneNumber = txtRePnumber.Text, Value = Convert.ToDecimal(txtReValue.Text), Date = Convert.ToDateTime(txtReDate.Text), State = 1 };
                var json = SerializationHelpers.SerializeJson(r);
                var resp = ResponseCallService(json, "POST", "Recharge");
                var userinfo = SerializationHelpers.DeserializeJson<Response>(resp);
                txtReResponse.Text = "Id Response: " + userinfo.response + "\n Response: " + userinfo.response + "\n Exception: " + ((userinfo.exception != null) ? userinfo.exception.Message : "");
            }
            catch (Exception ex)
            {
                txtReResponse.Text = "Oops... Something went wrong: \nException: " + ex.Message;
            }
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            try
            {
                var datacontractserializer = new DataContractJsonSerializer(typeof(Response));
                Price p = new Price { Id = Convert.ToInt32(cbPrice.SelectedValue), Description = "", Prices = 0 };
                var json = SerializationHelpers.SerializeJson(p);
                var resp = ResponseCallService(json, "POST", "getPricePerMinute");
                //var x = (Response)datacontractserializer.ReadObject(resp);
                var userinfo = SerializationHelpers.DeserializeJson<Response>(resp);
                txtPrResponse.Text = "Price: " + userinfo.idResponse + "\n Response: " + userinfo.response + "\n Exception: " + ((userinfo.exception != null) ? userinfo.exception.Message : "");
            }
            catch (Exception ex)
            {
                txtPrResponse.Text = "Oops... Something went wrong: \nException: " + ex.Message; 
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            try
            {
                var cusp = new CustomerPhone { Id = Convert.ToInt32(txtBalId.Text), PhoneNumber = txtBalPnumber.Text };
                var json = SerializationHelpers.SerializeJson(cusp);
                var resp = ResponseCallService(json, "POST", "GetPhoneBalance");
                var userinfo = SerializationHelpers.DeserializeJson<Response>(resp);
                txtBalResponse.Text = "Balance ID Response: " + userinfo.idResponse + "\n Balance Response: " + userinfo.response + "\n Exception: " + ((userinfo.exception != null) ? userinfo.exception.Message : "");
            }
            catch (Exception ex)
            {
                txtBalResponse.Text = "Oops... Something went wrong: \nException: " + ex.Message;
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            try
            {
                var call = new Call { PhoneNumber = txtCallFromNumber.Text, DestinationNumber = txtCallToPhoneNumber.Text };
                var json = SerializationHelpers.SerializeJson(call);
                var resp = ResponseCallService(json, "POST", "StartPhoneCall");
                var userinfo = SerializationHelpers.DeserializeJson<Response>(resp);
                txtCallResponse.Text = "Balance ID Response: " + userinfo.idResponse + "\n Balance Response: " + userinfo.response + "\n Exception: " + ((userinfo.exception != null) ? userinfo.exception.Message : "");
            }
            catch (Exception ex)
            {
                txtCallResponse.Text = "Oops... Something went wrong: \nException: " + ex.Message;
            }
        }

        private Stream ResponseCallService(string servicedata, string method, string transaction)
        {
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(servicedata);

                HttpWebRequest request;
                request = WebRequest.Create(url + "/" +  transaction) as HttpWebRequest;
                request.Timeout = 100 * 1000;
                request.Method = method; // "POST";
                request.ContentLength = data.Length;
                request.ContentType = "text/plain; charset=utf-8";

                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                var stream = response.GetResponseStream();
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Oops... Something went wrong: \nException: " + ex.Message);
            }
        }
    }
}
