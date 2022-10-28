﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Labb_3_WPF
{

    public partial class MainWindow : Window
    {
        public List<Booking> bookingList = new List<Booking>();
        public List<CheckDateAndTime> datumLista = new List<CheckDateAndTime>();
       


        public MainWindow()
        {
           
            // gör en lista med tider här och sen data binda dem
            InitializeComponent();
            AddDates(datumLista);






        }


   

        private void BookingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var förNamn = firstNameBox.Text;
                var efterNamn = lastNameBox.Text;
                var namn = $"{förNamn} {efterNamn}";
                var teleNr = phoneBox.Text;
                var kalenderDatum = MainCalendar.SelectedDate.Value.Date.ToShortDateString();
                DateOnly datum = DateOnly.Parse(kalenderDatum);
                var tid = TimeChoiceBox.Text.ToString();
                var kön = genderChoiceBox.Text.ToString();




                if (CheckInputs(förNamn, efterNamn, tid, kön, teleNr) == true)
                {
                    
                    
                    //string newTid = tid.Replace('.', ',');
                    //double RightTid = double.Parse(newTid);
                    //int teleIntNr;
                    //int.TryParse(teleNr, out teleIntNr);

                    string text = $"Namn: {namn}, Kön: {kön}, Telefonnummer: {teleNr}, Datum: {kalenderDatum}, Klockan: {tid}";

                    Boka(namn,kön,datum,tid,int.Parse(teleNr),text,datumLista,bookingList);
                    // gör en metod som kan lägga in variabler och gör det till en print text för lsitboxen

                   

                    //foreach (var item in datumLista)
                    //{
                    //    if (item.datum == datum)
                    //    {
                    //        for (int i = 0; i < item.Tider.Count; i++)
                    //        {
                    //            if (item.Tider[i].tid == tid)
                    //            {
                    //                checkTime = CheckTimeAvailable(item.Tider[i]);
                    //                i = item.Tider.Count;
                    //            }
                    //        }
                    //    }

                    //}

                    //if (checkTime == true)
                    //{
                    //    MessageBox.Show("it works!");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("aww man :C");
                    //}



                    //var queryDate = from item in datumLista
                    //                where item.datum == datum
                    //                select item;


                    //listBx.Items.Add(text);
                    //Booking bokning = new Booking(namn, kön, datum, RightTid, teleIntNr, text);
                    //bookingList.Add(bokning);

                    firstNameBox.Text = "";
                    lastNameBox.Text = "";
                    TimeChoiceBox.Text = "";
                    genderChoiceBox.Text = "";
                    phoneBox.Text = " ";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Välj ett tillgängligt Datum");

            }
        }


        public static void Boka(string namn, string kön, DateOnly datum, string tid, int nr, string text, List<CheckDateAndTime> datumLista, List<Booking> bookingList)
        {
            Booking bokning = new Booking(namn, kön, datum, tid, nr, text);
            bool checkTime;





            foreach (var item in datumLista)
            {
                if (item.datum == datum)
                {
                    for (int i = 0; i < item.Tider.Count; i++)
                    {
                        if (item.Tider[i].tid == tid)
                        {
                            checkTime = CheckTimeAvailable(item.Tider[i]);

                            if (checkTime == true)
                            {
                                bookingList.Add(bokning);
                                MessageBox.Show("it works!");
                            }
                            else
                            {
                                MessageBox.Show("aww man :C");
                            }

                            i = item.Tider.Count;
                        }
                    }
                }

            }


      



        }

        public static void AddDates(List<CheckDateAndTime> datumLista)
        {
            int daysToFill = 7;
            int startday = 14;
            int month = 11;
            int year = 2022;
            for (int i = 0; i < daysToFill; i++)
            {
                DateOnly date = new DateOnly(year, month, startday + i);
                CheckDateAndTime addDay = new CheckDateAndTime(date);
                datumLista.Add(addDay);
            }
        }
















        public static bool CheckTimeAvailable(Time tid)
        {
            if (tid.bordAvailable.Contains(" "))
            {
                for (int i = 0; i < tid.bordAvailable.Length; i++)
                {
                    if (tid.bordAvailable[i] == " ")
                    {
                        tid.bordAvailable[i] = "bokad";
                        i = tid.bordAvailable.Length;
                    }

                }
                return true;
            }
            else
            {
                MessageBox.Show("Denna tid är tyvärr fullbokad. prova en annnan.");
                return false;
            }
        }


        private void BookedDays_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            var date = BookedDays.SelectedDate.Value.Date.ToShortDateString();
            var pickedDay = DateOnly.Parse(date);
            foreach (var item in bookingList)
            {
                if (item.date == pickedDay)
                {
                    listBx.Items.Add(bookingList[0].text);
                }
            }

            
        }

     



        public static bool CheckInputs(string förNamn, string efterNamn, string tid, string kön, string telefonNr)
        {
            string missingText = "";

            List<string> missingInputs = new List<string>();

            if (förNamn == "") { missingInputs.Add($"Namn: {förNamn}"); }
            if (efterNamn == "") { missingInputs.Add($"Efternamn: {efterNamn}"); }
            if (telefonNr == "") { missingInputs.Add($"Telefon nummer: {telefonNr}"); }
            if (tid == "") { missingInputs.Add($"Tid: {tid}"); }
            if (kön == "") { missingInputs.Add($"Bord: {kön}"); }

            if (missingInputs.Count > 0)
            {
                for (int i = 0; i < missingInputs.Count; i++)
                {
                    missingText += $"{missingInputs[i]} Saknas\n";
                }
                MessageBox.Show($"Du har ej fyllt i alla rutor!\n{missingText}");
                return false;
            }
            else
            {
                return true;
            }




        }
    }

}




//public static List<Booking> PrebookedList()
//{


//    Booking booking1 = new Booking("Alex", new DateOnly(2022, 09, 25), 22.00, 5);
//    Booking booking2 = new Booking("Wilma", new DateOnly(2022, 09, 24), 21.00, 5);

//    bookingList.Add(booking1);
//    return bookingList;
//}






