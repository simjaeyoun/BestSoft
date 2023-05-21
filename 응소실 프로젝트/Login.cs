using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace 로그인화면
{
    public partial class Login : Form
    {
        public bool Login_Result; // Login 결과 변수

        ChromeOptions options;
        string loginUrl;
        ChromeDriver driver;


        StudentData studentData;

        public Login()
        {
            InitializeComponent();
            options = new ChromeOptions();
            options.AddArgument("window-size=1920x1080");
            options.AddArgument("headless");
            loginUrl = "https://klas.kw.ac.kr/usr/cmn/login/LoginForm.do?redirectUrl=/std/cmn/frame/Frame.do";

            try
            {
                //driver = new ChromeDriver();
                driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl(loginUrl);
            }
            catch (Exception e)
            {
                MessageBox.Show("error" + e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login_Result = true; // Login 버튼 클릭시 변수 기본값 true

            driver.FindElement(By.Id("loginId")).SendKeys(txtID.Text);
            driver.FindElement(By.Id("loginPwd")).SendKeys(txtPW.Text);

            driver.FindElement(By.ClassName("btn")).Click();
            try
            {
                //Wait main page
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                    By.XPath("/html/body/header/div[1]/div/div[2]/a[1]")));

                // Click main menu button
                driver.FindElement(By.XPath("/html/body/header/div[1]/div/div[1]/button")).Click();

                // Wait main menu
                WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait2.Until(ExpectedConditions.ElementExists(
                    By.XPath("/html/body/header/div[2]/div/div/div[1]/ul/li[2]/ul/li[2]/a")));

                // change 수강/성적 조회 url
                var link = driver.FindElement(By.XPath("/html/body/header/div[2]/div/div/div[1]/ul/li[2]/ul/li[2]/a"));
                var onclickAttr = link.GetAttribute("onclick");
                var urlStart = onclickAttr.IndexOf("'") + 1;
                var urlEnd = onclickAttr.IndexOf("'", urlStart);
                var url = onclickAttr.Substring(urlStart, urlEnd - urlStart);

                // Execute the linkUrl() function to change the URL
                ((IJavaScriptExecutor)driver).ExecuteScript($"linkUrl('{url}')");

                // Save student info
                var category = driver.FindElement(By.XPath("/html/body/main/div/div/div/div[2]/div/table[1]/tbody/tr/td[1]"));
                var major = driver.FindElement(By.XPath("/html/body/main/div/div/div/div[2]/div/table[1]/tbody/tr/td[2]"));
                var studentNum = driver.FindElement(By.XPath("/html/body/main/div/div/div/div[2]/div/table[1]/tbody/tr/td[3]"));
                var studentName = driver.FindElement(By.XPath("/html/body/main/div/div/div/div[2]/div/table[1]/tbody/tr/td[4]"));
                var studentState = driver.FindElement(By.XPath("/html/body/main/div/div/div/div[2]/div/table[1]/tbody/tr/td[5]"));


                string add = "127.0.0.1";

                studentData = new StudentData
                {
                    StudentName = studentName.Text,
                    StudentNum = Int32.Parse(studentNum.Text),
                    StudentState = studentState.Text,
                    StudentCategory = category.Text,
                    StudentMajor = major.Text,
                    Location = new Location { x = 0, y = 0 }
                };


                StudentManager.AddStudent(add, studentData);
            }
            catch
            {
                //Login falut
                Login_Result = false; // Login 실패 했을 때 False 설정


                WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                wait3.Until(ExpectedConditions.ElementExists(
                    By.XPath("/html/body/div[4]/div[2]/div[1]")));
                var pwfalut = driver.FindElement(By.XPath("/html/body/div[4]/div[2]/div[1]"));


                string LoginFalut = pwfalut.Text;

                // to find a number of login falut
                Match match = Regex.Match(LoginFalut, @"\d+");

                if (match.Success) // if password is falut
                    MessageBox.Show("개인 번호 또는 비밀번호가 일치하지 않습니다\n" + "로그인 실패 건수 " + match.Value + "회");
                else // if id is falut
                    MessageBox.Show("개인 번호 또는 비밀번호가 일치하지 않습니다.");

                // Click 확인button
                driver.FindElement(By.XPath("/html/body/div[4]/div[2]/div[2]/div/button")).Click();

                // if a number of login falut is over 5, restrict
                if (match.Value == "5")
                    MessageBox.Show("비밀번호 오류 5회이상으로 로그인을 제한합니다.");
                //clear the input
                driver.FindElement(By.Id("loginId")).Clear();
                driver.FindElement(By.Id("loginPwd")).Clear();
            }



            if (Login_Result) // Login 결과 변수 true일 때 정보 출력 후 Login Form Close
            {
                this.Close();
            }

        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}