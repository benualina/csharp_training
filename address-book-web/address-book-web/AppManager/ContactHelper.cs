﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace address_book_web
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {

        }

        public ContactHelper Createcontact(ContactData contact)
        {
            ContactCreation();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturtToHomepage();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
           
            IWebElement row = driver.FindElement(By.XPath("//tr[(@name='entry')]"));

            manager.Navigator.GotoHomepage();
            
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[(@name='entry')]"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                contacts.Add(new ContactData(element.Text));
            }
            return contacts;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GotoHomepage();
            SelectContact(v);
            DeleteContact();
            ReturtToHomepage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GotoHomepage();
            SelectContact(v);
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            ReturtToHomepage();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Homenumber);
            Type(By.Name("email"), contact.Email);
            return this;
        }

        public ContactHelper ContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper ReturtToHomepage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public ContactHelper ContactExistanceCheck(int index)
        {
            manager.Navigator.GotoHomepage();
            if (IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]")))
            {
                return this;
            }
            else
            {
                ApplicationManager app = ApplicationManager.GetInstanse();
                ContactData contact = new ContactData("Франчук");
                contact.Address = "г.Москва, ул Савеловская, дом 13а";
                contact.Company = "ООО Мороз";
                contact.Email = "324@gmail.com";
                contact.Homenumber = "+74956234133";
                app.Contacts.Createcontact(contact);
            }
            return this;
        }
    }

}
