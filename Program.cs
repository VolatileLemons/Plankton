using Microsoft.VisualBasic;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

int maxpostsize = 100;

string[] title = new string[maxpostsize]; //Used in item
string[] link = new string[maxpostsize]; //Used in item
string[] description = new string[maxpostsize]; //Used in item
string[] language = new string[maxpostsize];
string[] copyright = new string[maxpostsize];
string[] managingEditor = new string[maxpostsize];
string[] webMaster = new string[maxpostsize];
string[] pubDate = new string[maxpostsize]; //Used in item
string[] lastBuildDate = new string[maxpostsize];
string[] category = new string[maxpostsize]; //Used in item
string[] generator = new string[maxpostsize];
string[] docs = new string[maxpostsize];
string[] cloud = new string[maxpostsize];
string[] ttl = new string[maxpostsize];
string[] image = new string[maxpostsize];
string[] rating = new string[maxpostsize];
string[] textInput = new string[maxpostsize];
string[] skipHours = new string[maxpostsize];
string[] skipDays = new string[maxpostsize];
string[] author = new string[maxpostsize]; //Used in item
string[] comments = new string[maxpostsize]; //Used in item
string[] enclosure = new string[maxpostsize]; //Used in item
string[] guid = new string[maxpostsize]; //Used in item
string[] source = new string[maxpostsize]; //Used in item

XElement[] item = new XElement[maxpostsize];

StreamReader sr = new StreamReader("default");

title[0] = sr.ReadLine();
link[0] = sr.ReadLine();
description[0] = sr.ReadLine();
language[0] = sr.ReadLine();
copyright[0] = sr.ReadLine();
managingEditor[0] = sr.ReadLine();
webMaster[0] = sr.ReadLine();
pubDate[0] = sr.ReadLine();
lastBuildDate[0] = sr.ReadLine();
category[0] = sr.ReadLine();
generator[0] = sr.ReadLine();
docs[0] = sr.ReadLine();
cloud[0] = sr.ReadLine();
ttl[0] = sr.ReadLine();
image[0] = sr.ReadLine();
rating[0] = sr.ReadLine();
textInput[0] = sr.ReadLine();
skipHours[0] = sr.ReadLine();
skipDays[0] = sr.ReadLine();

string url = sr.ReadLine();

XElement feed =
    new XElement("rss",
        new XElement("channel",
            new XElement("title", title[0]),
            new XElement("link", link[0]),
            new XElement("description", description[0]),
            new XElement("language", language[0]),
            new XElement("copyright", copyright[0]),
            new XElement("managingEditor", managingEditor[0]),
            new XElement("webMaster", webMaster[0]),
            new XElement("pubDate", pubDate[0]),
            new XElement("lastBuildDate", lastBuildDate[0]),
            new XElement("category", category[0]),
            new XElement("generator", generator[0]),
            new XElement("docs", docs[0]),
            new XElement("cloud", cloud[0]),
            new XElement("ttl", ttl[0]),
            new XElement("image", image[0]),
            new XElement("rating", rating[0]),
            new XElement("textInput", textInput[0]),
            new XElement("skipHours", skipHours[0]),
            new XElement("skipDays", skipDays[0])
        )
    );

//feed.Element("channel").Add(item);

//Console.ForegroundColor = ConsoleColor.DarkMagenta;

static string finder(string strSource, string strStart, string strEnd)
{
    const int kNotFound = -1;

    var startIdx = strSource.IndexOf(strStart);
    if (startIdx != kNotFound)
    {
        startIdx += strStart.Length;
        var endIdx = strSource.IndexOf(strEnd, startIdx);
        if (endIdx > startIdx)
        {
            return strSource.Substring(startIdx, endIdx - startIdx);
        }
    }
    return String.Empty;
}

/*
using WebClient client = new WebClient();
client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
Stream data = client.OpenRead(url);
StreamReader reader = new StreamReader(data);
string fullsitepull = reader.ReadToEnd();

string post = finder(fullsitepull, "/p/", "/");
*/

IWebDriver driver = new ChromeDriver();
driver.Navigate().GoToUrl(url);
//var title2 = driver.Title;

//x1i10hfl xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x972fbf xcfux6l x1qhh985 xm0m39n x9f619 x1ypdohk xt0psk2 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz _a6hd

//does find class for some reason need to try with single word class on the dom

IWebElement fruit = driver.FindElement(By.XPath("//div[@class='x1i10hfl xjbqb8w x1ejq31n xd10rxx x1sy0etr x17r0tee x972fbf xcfux6l x1qhh985 xm0m39n x9f619 x1ypdohk xt0psk2 xe8uvvx xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x16tdsg8 x1hl2dhg xggy1nq x1a2a7pz _a6hd']"));

XElement xmlTree2 = new XElement("Root",
    from el in feed.Elements()
    select el
);
Console.WriteLine(xmlTree2);
//Console.Write(fullsitepull);
//Console.WriteLine(post);
//Console.WriteLine(title2);
Console.Write(fruit.Text);

driver.Quit();