using Microsoft.VisualBasic;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chromium;
using AngleSharp.Text;
using System.Diagnostics;
using System.Net.Http;
using OpenQA.Selenium.Support.Extensions;
using System.Text.Json;

StreamReader sr = new StreamReader("default");
HttpClient client = new HttpClient();
string[] settings = new string[22];
int line = 0;
string currentresponse = "";

async Task httppostimg(string key, Screenshot screenshot)
{
    var imageBytes = screenshot.AsByteArray;
    var bytestring = Convert.ToBase64String(imageBytes);
    var values = new Dictionary<string, string>
    {
        { "image", bytestring }
    };

    var content = new FormUrlEncodedContent(values);
    var response = await client.PostAsync("https://api.imgbb.com/1/upload?expiration=15552000&key=" + key, content);
    var responseString = await response.Content.ReadAsStringAsync();
    currentresponse = responseString;
}

while (!sr.EndOfStream)
{
    settings[line] = sr.ReadLine();
    line += 1;
}

int maxpostsize = Int32.Parse(settings[20]); ;

string[] title = new string[maxpostsize + 1]; //Used in item
string[] link = new string[maxpostsize + 1]; //Used in item
string[] description = new string[maxpostsize + 1]; //Used in item
string[] language = new string[1];
string[] copyright = new string[1];
string[] managingEditor = new string[1];
string[] webMaster = new string[1];
string[] pubDate = new string[maxpostsize + 1]; //Used in item
string[] lastBuildDate = new string[1];
string[] category = new string[maxpostsize + 1]; //Used in item
string[] generator = new string[1];
string[] docs = new string[1];
string[] cloud = new string[1];
string[] ttl = new string[1];
string[] image = new string[1];
string[] rating = new string[1];
string[] textInput = new string[1];
string[] skipHours = new string[1];
string[] skipDays = new string[1];
string[] author = new string[maxpostsize + 1]; //Used in item
string[] comments = new string[maxpostsize + 1]; //Used in item
string[] enclosure = new string[maxpostsize + 1]; //Used in item
string[] guid = new string[maxpostsize + 1]; //Used in item
string[] source = new string[maxpostsize + 1]; //Used in item

XElement[] item = new XElement[maxpostsize];

title[0] = settings[0];
link[0] = settings[1];
description[0] = settings[2];
language[0] = settings[3];
copyright[0] = settings[4];
managingEditor[0] = settings[5];
webMaster[0] = settings[6];
pubDate[0] = settings[7];
lastBuildDate[0] = settings[8];
category[0] = settings[9];
generator[0] = settings[10];
docs[0] = settings[11];
cloud[0] = settings[12];
ttl[0] = settings[13];
image[0] = settings[14];
rating[0] = settings[15];
textInput[0] = settings[16];
skipHours[0] = settings[17];
skipDays[0] = settings[18];

string url = settings[19];

XElement feed =
    new XElement("rss",
        new XElement("channel",
            new XElement("title", title[0]),
            new XElement("link", link[0]),
            new XElement("description", description[0]),
            new XElement("language", language[0]),
            //new XElement("copyright", copyright[0]),
            new XElement("managingEditor", managingEditor[0]),
            new XElement("webMaster", webMaster[0]),
            new XElement("pubDate", pubDate[0]),
            //new XElement("lastBuildDate", lastBuildDate[0]),
            //new XElement("category", category[0]),
            new XElement("generator", generator[0]),
            new XElement("docs", docs[0]),
            //new XElement("cloud", cloud[0]),
            new XElement("ttl", ttl[0]),
            new XElement("image")
            //new XElement("rating", rating[0]),
            //new XElement("textInput", textInput[0]),
            //new XElement("skipHours", skipHours[0]),
            //new XElement("skipDays", skipDays[0])
        )
    );

feed.SetAttributeValue("version", "2.0");
feed.Element("channel").Element("image").SetAttributeValue("title", "image");
feed.Element("channel").Element("image").SetAttributeValue("url", image[0]);
feed.Element("channel").Element("image").SetAttributeValue("link", image[0]);

IWebDriver driver = new ChromeDriver();
driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
driver.Url = url;
driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("sessionid", "55937637076%3Ap9pcaA0gscI2pI%3A23%3AAYcP60XCF7Hzx5KrSNmoO5IytNsHyWBwmkc9eDHbyA"));
driver.Navigate().GoToUrl(url);

IWebElement[] fruits = new IWebElement[maxpostsize];

for (int i = 0; i < maxpostsize; i++)
{
    fruits[i] = driver.FindElement(By.CssSelector("div._ac7v:nth-child(1) > div:nth-child(" + (i + 1).ToString() + ") > a:nth-child(1)"));
}

for (int i = 0; i < maxpostsize; i++)
{
    var currentlink = fruits[i].GetAttribute("href");
    var img = fruits[i].FindElement(By.XPath("./child::*[1]/child::*[1]/child::*[1]")).GetAttribute("src");
    driver.Navigate().GoToUrl(currentlink);
    title[i + 1] = driver.Title;
    link[i + 1] = currentlink;
    description[i + 1] = driver.FindElement(By.CssSelector("h1._ap3a")).Text;
    //pubDate[i + 1] = fruits[i].Text;
    //category[i + 1] = fruits[i].Text;
    //author[i + 1] = driver.FindElement(By.CssSelector("a._acan")).Text;
    //comments[i + 1] = fruits[i].Text;
    //guid[i + 1] = fruits[i].Text;
    //enclosure[i + 1] = fruits[i].Text;
    source[i + 1] = url;
    item[i] =
        new XElement("item",
            new XElement("title", title[i + 1]),
            new XElement("link", link[i + 1]),
            new XElement("description", description[i + 1]),
            //new XElement("pubDate", pubDate[i + 1]),
            //new XElement("category", category[i + 1]),
            //new XElement("author", author[i + 1]),
            //new XElement("comments", comments[i + 1]),
            //new XElement("guid", guid[i + 1]),
            new XElement("enclosure", enclosure[i + 1]),
            new XElement("source", source[i + 1])
        );
    item[i].Element("source").SetAttributeValue("url", source[i + 1]);
    feed.Element("channel").Add(item[i]);

    driver.Navigate().GoToUrl(img);
    var imagedata = driver.TakeScreenshot();

    await httppostimg(settings[21], imagedata);
    var json = JsonDocument.Parse(currentresponse);
    item[i].Element("enclosure").SetAttributeValue("url", json.RootElement.GetProperty("data").GetProperty("url").GetString());
    item[i].Element("enclosure").SetAttributeValue("length", json.RootElement.GetProperty("data").GetProperty("size").GetInt32().ToString());
    item[i].Element("enclosure").SetAttributeValue("type", json.RootElement.GetProperty("data").GetProperty("image").GetProperty("mime").GetString());

}

static void cleanup(XElement original)
{
    original.Descendants()
   .Where(a => a.IsEmpty || String.IsNullOrWhiteSpace(a.Value))
   .Remove();
}

//cleanup(feed);

XElement xmlTree2 = new XElement("rss",
    from el in feed.Elements()
    select el
);
xmlTree2.SetAttributeValue("version", "2.0");
Console.WriteLine(xmlTree2);
Console.ReadLine();

driver.Quit();