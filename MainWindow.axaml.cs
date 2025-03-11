using Avalonia.Controls;
using System.Collections.Generic;
using System.Linq;
using style.Entities;
using Avalonia;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Text;


namespace style;

public partial class MainWindow : Window
{
    public List<Appointment> Appointments {get;set;}
    public List<Employee> Employees {get;set;}
    public List<Client> Clients {get;set;}
    PostgresContext context;
    public MainWindow()
    {
        InitializeComponent();
        context = new PostgresContext();
        ShowTable();
        ShowTable2();
        ShowTable3();
        Appointments = context.Appointments.ToList();
        var appointments = this.FindControl<DataGrid>("dataGridAppointment");
        dataGridAppointment.ItemsSource = Appointments;
        var emloyee = this.FindControl<DataGrid>("dataGridEmployee");
        dataGridEmployee.ItemsSource = Employees;
        var clients = this.FindControl<DataGrid>("dataGridClient");
        dataGridClient.ItemsSource = Clients;
    }
    public void ShowTable()
    {
        Appointments = context.Appointments
                                .Include(x => x.Client)
                                .Include(x => x.Employee)
                                .Include(x => x.Service)
                                .ToList();
        dataGridAppointment.ItemsSource = Appointments;
    }

    public void ShowTable2()
    {
        Clients = context.Clients.ToList();
        dataGridClient.ItemsSource = Clients;
    }
    public void ShowTable3()
    {
        Employees = context.Employees.ToList();
        dataGridEmployee.ItemsSource = Employees;
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        var newAppointment = new Appointment();
        context.Appointments.Add(newAppointment);
        AppoinmentAdd appointmentAddWindow = new AppoinmentAdd(this.context, newAppointment);
        appointmentAddWindow.ShowDialog(this);
    
    }
     public void btnDel_Click(object sender, RoutedEventArgs e)
    {
        var appointments = dataGridAppointment.SelectedItem as Appointment;
        if (appointments == null)
        return;
        try
        {
            context.Appointments.Remove(appointments);
            context.SaveChanges();
        }
        catch (System.Exception)
        {
            Console.Write("Error");
        }
    }
    private void btnFilter_Click(object sender, RoutedEventArgs e)
    {
        var dateStartPicker = this.FindControl<DatePicker>("dateStartFilter");
        var dateEndPicker = this.FindControl<DatePicker>("dateEndFilter");
        var dateStart = new DateOnly(dateStartPicker.SelectedDate.Value.Date.Year, dateStartPicker.SelectedDate.Value.Date.Month,dateStartPicker.SelectedDate.Value.Date.Day);
        var dateEnd = new DateOnly(dateEndPicker.SelectedDate.Value.Date.Year, dateEndPicker.SelectedDate.Value.Date.Month,dateEndPicker.SelectedDate.Value.Date.Day);
        Appointments = context.Appointments 

                                .Where(x => x.AppointmentDate >= dateStart && x.AppointmentDate <= dateEnd) 
                                .Include(x => x.Client)
                                .Include(x => x.Employee)
                                .Include(x => x.Service)
                                .ToList();
        dataGridAppointment.ItemsSource = Appointments;

    }
    private void btnAll_Click(object sender, RoutedEventArgs e)
    {
        Appointments = context.Appointments
                                .Include(x => x.Client)
                                .Include(x => x.Employee)
                                .Include(x => x.Service)
                                .ToList();
        dataGridAppointment.ItemsSource = Appointments;
    }
    private void TextBox_TextChanged(object sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        var numberList = this.FindControl<DataGrid>("dataGridClient");
        var searchText = this.FindControl<TextBox>("searchBox");
        if (searchText == null)
        return;
        Clients = context.Clients.Where(x => x.LastName.ToLower().Contains(searchText.Text.ToLower())).ToList();
        numberList.ItemsSource = Clients;
    }
}