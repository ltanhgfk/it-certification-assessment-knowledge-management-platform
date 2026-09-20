using System;
using System.Data;
//using System.Configuration;
using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Security.Cryptography;
////using System.Linq;
using System.Text;
//using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
//using System.Drawing.Drawing2D;
using Microsoft.Office.Interop.Word;
using RTF;
using System.Drawing;

namespace MTTestClient
{
    class Globals
    {
        public static void BuilderCode(RTFBuilderbase sb, string file)
        {
            //object missing = Type.Missing;
            object missing = System.Reflection.Missing.Value;
            object FileName = file;
            object readOnly = true;
            //RichTextBox rtb = new RichTextBox();
            Microsoft.Office.Interop.Word.Application m_word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document docs = m_word.Documents.Open(ref FileName,
                                    ref missing, ref readOnly, ref missing, ref missing,
                                    ref missing, ref missing, ref missing, ref missing,
                                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            string tenmodun = "";
            string cauhoi = "";
            int daophuongan = 0;
            int questionNumber = 0;
            int optionNumber = 0;
            int AnswerNumber = 0;
            string line = "";
            string paa = "";
            string pab = "";
            string pac = "";
            string pad = "";
            string sql = "";
            int kq = 0;
            string restText = "";
            string regStr = "";
            string strTextImage = "";
            string paraStr = "";
            int count = 0;
            int sttQuestion = 0;
            for (int i = 0; i < docs.Paragraphs.Count; i++)
            {
                //get the paragraph we want to check
                Microsoft.Office.Interop.Word.Paragraph para = docs.Paragraphs[i + 1];
                if (para != null && para.Range.Text.Trim().Length >= 1)
                {
                    paraStr = para.Range.Text.Trim();
                    if (paraStr.Length >= 1)
                    {
                        //if (paraStr.Substring(0, 5) == "<mod>")
                        if (paraStr.Contains("<mod>"))
                        {
                            regStr = paraStr.Substring(0, 5);
                            restText = paraStr.Substring(5, paraStr.Length - 5);
                            //tenmodun = line.Substring(5, line.Length - 5);
                            string countquestion = "select count(*) from  Tracnghiem where TenMoDun=@tenmodun";
                            count = (int)Database.ExecuteScalar(countquestion, "@tenmodun", restText);

                            questionNumber = 0;
                        }
                        //else if (paraStr.Substring(0, 4) == "<00>" || paraStr.Substring(0, 4) == "<11>")
                        else if (paraStr.Contains("<00>") || paraStr.Contains("<11>"))
                        {
                            count = count + 1;
                            questionNumber = questionNumber + 1;
                            regStr = paraStr.Substring(0, 4);
                            restText = paraStr.Substring(4, paraStr.Length - 4);
                        }
                        //else if (paraStr.Substring(0, 4) == "<**>")
                        else if (paraStr.Contains("<**>"))
                        {
                            regStr = paraStr.Substring(0, 4);
                            restText = paraStr.Substring(4, paraStr.Length - 4);
                        }
                        else
                        {
                            regStr = "";
                            restText = paraStr;
                        }
                    }
                    //else if (paraStr != " ")
                    //{
                    //    regStr = "";
                    //    restText = paraStr;
                    //}

                    if (para.Range.InlineShapes.Count >= 1)//tuc la có hình ảnh trong đoạn đó
                    {
                        //the InlineShapes collection exists in the Range of that paragraph
                        foreach (Microsoft.Office.Interop.Word.InlineShape ils in para.Range.InlineShapes)
                        {
                            int lenRestText = restText.Length;
                            int PosOfSlash = restText.IndexOf("/");
                            string pretext = restText.Substring(0, PosOfSlash);
                            restText = restText.Substring(PosOfSlash + 1, lenRestText - PosOfSlash - 1);
                            byte[] bytpretext = System.Text.Encoding.UTF8.GetBytes(pretext);

                            strTextImage = strTextImage + Convert.ToBase64String(bytpretext);
                            sb.Append(pretext);
                            if (ils != null)
                            {
                                //validate this is a picture
                                if (ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePicture)
                                {
                                    //ils.Height = 300;
                                    //ils.Width = 300;
                                    //select the shape
                                    ils.Select();
                                    //copy the selection as a picture
                                    m_word.Selection.CopyAsPicture();
                                    //get the object data from the clipboard
                                    IDataObject ido = Clipboard.GetDataObject();
                                    if (ido != null)
                                    {
                                        //can convert to bitmap?
                                        if (ido.GetDataPresent(DataFormats.Bitmap))
                                        {
                                            //cast the data into a bitmap object
                                            Bitmap bmp = (Bitmap)ido.GetData(DataFormats.Bitmap);
                                            ///string strImg = Encoding.ASCII.GetString(imageToByteArray(bmp));
                                            string strImg = imageToBase64String(bmp);
                                            if (strTextImage != "" && strTextImage != " ") strTextImage = strTextImage + "##BegImg##" + strImg + "##EndImg##";
                                            else strTextImage = "##BegImg##" + strImg + "##EndImg##";

                                            sb.InsertImage(bmp);
                                        }
                                    }
                                }
                            }
                        }//Ket thuc lap so lan anh có trong doan para
                        if (restText != "" && restText != " ")
                        {
                            byte[] bytrestText = System.Text.Encoding.UTF8.GetBytes(restText);
                            strTextImage = strTextImage + Convert.ToBase64String(bytrestText);
                            //Doi nguoc lai => var str = System.Text.Encoding.Default.GetString(result);result la kieu byte[]
                        }
                        sb.AppendLine(restText);
                        //sb.AppendLine(docs.Paragraphs[i + 1].Range.Text);                    
                    }//nguoc lai neu khong co hinh anh trong doan paraStr;
                    else
                    {
                        if (questionNumber != 0)
                        {
                            byte[] bytrestText = System.Text.Encoding.UTF8.GetBytes(restText);
                            strTextImage = Convert.ToBase64String(bytrestText);
                        }
                        else strTextImage = restText;
                        sb.AppendLine(paraStr);
                    }
                    //Viet code so sanh de insert vo database
                    //line = regStr + sb.ToString().Trim();
                    line = regStr + strTextImage;
                    if (line.Length >= 1)
                    {
                        //if (line.Substring(0, 5) == "<mod>")
                        if (line.Contains("<mod>"))
                        {
                            tenmodun = line.Substring(5, line.Length - 5);
                            //questionNumber = 0;
                        }
                        //if (line.Substring(0, 4) == "<00>" || (line.Substring(0, 4) == "<11>"))
                        if (line.Contains("<00>") || line.Contains("<11>"))
                        {
                            //questionNumber = questionNumber + 1;
                            cauhoi = line.Substring(4, line.Length - 4);
                            daophuongan = Convert.ToInt32(line.Substring(1, 1));
                            Database.ExecuteNonQuery("insert into TracNghiem(Cauhoi,NoiDung,DaoDapAn,TenModun,SoDapAn) values(@Cauhoi,@NoiDung,@DaoDapAn,@TenModun,@SoDapAn)", "@Cauhoi", tenmodun + "_" + count.ToString("00"), "@NoiDung", cauhoi, "@DaoDapAn", daophuongan, "@TenModun", tenmodun, "@SoDapAn", optionNumber);
                            strTextImage = "";
                            optionNumber = 0;
                            AnswerNumber = 0;
                        }
                        //else if (line.Substring(0, 4) == "<**>")
                        else if (line.Contains("<**>"))
                        {
                            optionNumber = optionNumber + 1;
                            //AnswerNumber = optionNumber;
                            //if (AnswerNumber == 1)
                            //{
                            //    paa = line.Substring(4, line.Length - 4);
                            //}
                            //else if (AnswerNumber == 2)
                            //{
                            //    pab = line.Substring(4, line.Length - 4);
                            //}
                            //else if (AnswerNumber == 3)
                            //{
                            //    pac = line.Substring(4, line.Length - 4);
                            //}
                            //else
                            //    pad = line.Substring(4, line.Length - 4);
                            Database.ExecuteNonQuery("Insert into ChiTietTracNghiem(NoiDungPA,Cauhoi,Position) values(@NoiDungPA,@Cauhoi,@Position)", "@NoiDungPA", line.Substring(4, line.Length - 4), "@Cauhoi", tenmodun + "_" + count.ToString("00"), "@Position", optionNumber);
                            strTextImage = "";
                            //int max = (int)Database.ExecuteScalar("Select max(MaPa) from ChiTietTracNghiem");
                            //Database.ExecuteNonQuery("Update TracNghiem set DapAnDung=@dapandung where Cauhoi=@cauhoi", "@dapandung", max, "@Cauhoi", tenmodun + "_" + count.ToString("00"));
                            Database.ExecuteNonQuery("Update TracNghiem set DapAnDung=@dapandung where Cauhoi=@cauhoi", "@dapandung", optionNumber, "@Cauhoi", tenmodun + "_" + count.ToString("00"));
                        }
                        else
                        {
                            optionNumber = optionNumber + 1;
                            //if (optionNumber == 1)
                            //{
                            //    paa = line.Substring(0, line.Length);
                            //}
                            //else if (optionNumber == 2)
                            //{
                            //    pab = line.Substring(0, line.Length);
                            //}
                            //else if (optionNumber == 3)
                            //{
                            //    pac = line.Substring(0, line.Length);
                            //}
                            //else
                            //    pad = line.Substring(0, line.Length);
                            Database.ExecuteNonQuery("Insert into ChiTietTracNghiem(NoiDungPA,Cauhoi,Position) values(@NoiDungPA,@Cauhoi,@Position)", "@NoiDungPA", line.Substring(0, line.Length), "@Cauhoi", tenmodun + "_" + count.ToString("00"), "@Position", optionNumber);
                            strTextImage = "";
                        }
                    }

                    //else if (line != " ")
                    //{
                    //    optionNumber = optionNumber + 1;
                    //    //if (optionNumber == 1)
                    //    //{
                    //    //    paa = line.Substring(0, line.Length);
                    //    //}
                    //    //else if (optionNumber == 2)
                    //    //{
                    //    //    pab = line.Substring(0, line.Length);
                    //    //}
                    //    //else if (optionNumber == 3)
                    //    //{
                    //    //    pac = line.Substring(0, line.Length);
                    //    //}
                    //    //else
                    //    //    pad = line.Substring(0, line.Length);
                    //    Database.ExecuteNonQuery("Insert into ChiTietTracNghiem(NoiDungPA,Cauhoi,Position) values(@NoiDungPA,@Cauhoi,@Position)", "@NoiDungPA", line.Substring(0, line.Length), "@Cauhoi", tenmodun + "_" + count.ToString("00"), "@Position", optionNumber);
                    //}

                    //if (optionNumber == 4)
                    //{
                    //    sql = "insert into TracNghiem(Cauhoi,NoiDung,PhuongAn_A,PhuongAn_B,PhuongAn_C,PhuongAn_D,SoDapAn,DapAnDung,DaoDapAn,TenModun,MucDo) values(@Cauhoi,@NoiDung,@PhuongAn_A,@PhuongAn_B,@PhuongAn_C,@PhuongAn_D,@SoDapAn,@DapAnDung,@DaoDapAn,@TenModun,@MucDo)";
                    //    //'" + tenmodun + "_" + questionNumber.ToString() + "','" + cauhoi + "','" + paa + "','" + pab + "','" + pac + "','" + pad + "'," + 4 + ",'" + daocauhoi + "','" + tenmodun + "','nc'
                    //    kq = Database.ExecuteNonQuery(sql, "@Cauhoi", tenmodun + "_" + count.ToString("00"), "@NoiDung", cauhoi, "@PhuongAn_A", paa, "@PhuongAn_B", pab, "@PhuongAn_C", pac, "@PhuongAn_D", pad, "@SoDapAn", 4, "@DapAnDung", AnswerNumber, "@DaoDapAn", daophuongan, "@TenModun", tenmodun, "@MucDo", "nc");
                    //    strTextImage = "";                        
                    //}
                    //Het viet code so sanh de insert vo database
                }
            }
            docs.Close();
            m_word.Quit();
            MessageBox.Show("Hoàn thành việc cập nhật bộ đề thi!");
        }

        public static void DisplayDethiOnRtf(RTFBuilderbase sb, string ketcaude,string sttstr)
        {
            string[] mangkitu = { "a", "b", "c", "d", "e", "f", "g", "h" };
            string[] mangdegoc;
            int stt = 0;
            string[] mangstt = sttstr.Split('_');
            //string ketcaude = (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe = @made", "@made", made);
            mangdegoc = ketcaude.Split('#');
            for (int i = 0; i < mangdegoc.Length - 1; i++)
            {
                if (mangstt.Length >= 3)
                {
                    if (i == 0)
                    {
                        sb.FontSize(40).AppendLine("PHẦN THI: " + mangstt[0].Substring(mangstt[0].IndexOf('-') + 1, mangstt[0].Length - (mangstt[0].IndexOf('-') + 1)));
                        sb.FontSize(40).AppendLine("---------------------------------------------------------------------------------------");
                    }
                    else if (i == Convert.ToInt32(mangstt[0].Substring(0, mangstt[0].IndexOf('-'))))
                    {
                        sb.FontSize(40).AppendLine("---------------------------------------------------------------------------------------");
                        sb.FontSize(40).AppendLine("PHẦN THI: " + mangstt[1].Substring(mangstt[1].IndexOf('-') + 1, mangstt[1].Length - (mangstt[1].IndexOf('-') + 1)));
                        sb.FontSize(40).AppendLine("---------------------------------------------------------------------------------------");
                    }
                    else if (i == Convert.ToInt32(mangstt[1].Substring(0, mangstt[1].IndexOf('-'))))
                    {
                        sb.FontSize(40).AppendLine("---------------------------------------------------------------------------------------");
                        sb.FontSize(40).AppendLine("PHẦN THI: " + mangstt[2].Substring(mangstt[2].IndexOf('-') + 1, mangstt[2].Length - (mangstt[2].IndexOf('-') + 1)));
                        sb.FontSize(40).AppendLine("---------------------------------------------------------------------------------------");
                    }
                }
                stt = stt + 1;
                string cauhoi = mangdegoc[i].Substring(0, mangdegoc[i].IndexOf(";"));
                string noidungch = (string)Database.ExecuteScalar("Select NoiDung from TracNghiem where CauHoi =@cauhoi", "@cauhoi", cauhoi);
                //sb.AppendLine(convertBase64ToString(noidungch));
                Globals.lineProcessing(noidungch, sb, "Câu " + stt);
                int soluongphuongan = Convert.ToInt32(mangdegoc[i].Substring(mangdegoc[i].LastIndexOf(";") - 1, 1));
                string positionStr = mangdegoc[i].Substring(mangdegoc[i].IndexOf(";") + 1, soluongphuongan * 2);
                for (int j = 0; j < soluongphuongan; j++)
                {
                    string pos = positionStr.Substring(0, 1);
                    string noidungpa = (string)Database.ExecuteScalar("Select NoiDungPA From ChiTietTracNghiem where CauHoi=@cauhoi and Position=@position", "@cauhoi", cauhoi, "@position", pos);
                    //sb.AppendLine(convertBase64ToString(noidungpa));
                    Globals.lineProcessing(noidungpa, sb, "      " + mangkitu[j]);
                    positionStr = positionStr.Substring(2, positionStr.Length - 2);
                }
            }
        }

        public static void lineProcessing(string restText, RTFBuilderbase sb, string pre)
        {
            //tring restText = line;
            if (restText.Contains("##BegImg##") == true)//tuc la có hình ảnh trong đoạn đó
            {
                //the InlineShapes collection exists in the Range of that paragraph
                int i = 0;
                while (restText.Contains("##BegImg##"))
                {
                    int lenRestText = restText.Length;
                    int PosBegin = restText.IndexOf("##BegImg##");
                    int PosEnd = restText.IndexOf("##EndImg##");
                    string pretext = restText.Substring(0, PosBegin);
                    string img = restText.Substring(PosBegin + 10, PosEnd - PosBegin - 10);

                    restText = restText.Substring(PosEnd + 10, lenRestText - PosEnd - 10);

                    //byte[] bytpretext = System.Text.Encoding.UTF8.GetBytes(pretext);
                    //strTextImage = strTextImage + Convert.ToBase64String(bytpretext);

                    if (pre.Contains("Câu"))
                    {
                        if (i == 0)
                        {
                            sb.FontSize(20).FontStyle(FontStyle.Bold).Append(pre + ". " + convertBase64ToString(pretext));
                        }
                        else sb.FontSize(20).FontStyle(FontStyle.Bold).Append(convertBase64ToString(pretext));

                    }
                    else
                    {
                        if (i == 0)
                        {
                            sb.FontSize(20).Append(pre + ". " + convertBase64ToString(pretext));
                        }
                        else sb.FontSize(20).Append(convertBase64ToString(pretext));
                    }
                    sb.FontSize(20).InsertImage(Base64ToImage(img));
                    i++;

                }//Ket thuc lap so lan anh có trong doan para     
                if (pre.Contains("Câu"))
                {
                    //sb.FontSize(20).FontStyle(FontStyle.Bold).AppendLine(restText);
                    sb.FontSize(20).FontStyle(FontStyle.Bold).AppendLine(convertBase64ToString(restText));                    
                    sb.AppendLine();
                }
                else
                {
                    //sb.FontSize(20).AppendLine(restText);
                    sb.FontSize(20).AppendLine(convertBase64ToString(restText));
                    sb.AppendLine();
                }
                //sb.AppendLine(docs.Paragraphs[i + 1].Range.Text);                    
            }//nguoc lai neu khong co hinh anh trong doan paraStr;
            else
            {
                if (pre.Contains("Câu"))
                {
                    sb.FontSize(20).FontStyle(FontStyle.Bold).AppendLine(pre + ". " + convertBase64ToString(restText));
                    sb.AppendLine();
                }
                else
                {
                    sb.FontSize(20).AppendLine(pre + ". " + convertBase64ToString(restText));
                    sb.AppendLine();
                }
            }
        }

        public static string convertBase64ToString(string strModified)
        {
            byte[] b = Convert.FromBase64String(strModified);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        public static string imageToBase64String(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return Convert.ToBase64String(ms.ToArray());
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public static List<int> GetRandomNumbers(int count)
        {
            List<int> randomNumbers = new List<int>();
            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                int number;

                do number = r.Next(1, count);
                while (randomNumbers.Contains(number));
                randomNumbers.Add(number);
            }
            return randomNumbers;
        }

        public static string GetRandomNumbersString(List<int> randomNumbers, int answer)
        {
            string NumbersString = ";";
            int newanswer = 0;
            Random r = new Random();

            for (int j = 0; j < randomNumbers.Count; j++)
            {
                NumbersString = NumbersString + randomNumbers[j].ToString() + ";";
                if (randomNumbers[j] == answer) newanswer = j + 1;
            }
            return NumbersString + newanswer + ";";
        }

        //public void ConvertToSeconds(int minutes)
        //{
        //}

        //public void ConvertToMinutes(int seconds)
        //{
        //}
    }
}

