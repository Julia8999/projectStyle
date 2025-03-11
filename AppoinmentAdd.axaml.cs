using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using style.Entities;
using System.Linq;
using System;

namespace style;

public partial class AppoinmentAdd : Window
{
    PostgresContext Context {get; set; }
    public Appointment CurrentAppointment {get; set; }
    public AppoinmentAdd(PostgresContext context, Appointment currentAppointment)
    {
        InitializeComponent();
        this.Context = context;
        this.CurrentAppointment = currentAppointment;
        FillClients();
        FillEmployees();
        FillServices();
    }
     public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public void FillClients()
    {
        var clients = Context.Clients.ToList();
        var cmbClientList = this.FindControl<ComboBox>("cmbClient");
        cmbClientList.ItemsSource = clients;
    }
    public void FillEmployees()
    {
        var employees = Context.Employees.ToList();
        var cmbEmployeeList = this.FindControl<ComboBox>("cmbEmployee");
        cmbEmployeeList.ItemsSource = employees;
    }

    public void FillServices()
    {
        var services = Context.Services.ToList();
        var cmbServiceList = this.FindControl<ComboBox>("cmbService");
        cmbServiceList.ItemsSource = services;
    }
    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        
            var cmbClientsList = this.FindControl<ComboBox>("cmbClient");
            var cmbEmployeesList = this.FindControl<ComboBox>("cmbEmployee");
            var cmbServicesList = this.FindControl<ComboBox>("cmbService");
            var datesList = this.FindControl<DatePicker>("date");
            

            var client = cmbClientsList.SelectedItem as Client;
            var employee = cmbEmployeesList.SelectedItem as Employee;
            var service = cmbServicesList.SelectedItem as Service;
            
            var currentDate = datesList.SelectedDate.Value.Date;

           
            try
            {
                this.CurrentAppointment.ClientId = client.ClientId;
                this.CurrentAppointment.EmployeeId = employee.EmployeeId;
                this.CurrentAppointment.ServiceId = service.ServiceId;
                this.CurrentAppointment.AppointmentDate = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);
                Context.SaveChanges();
            }
            catch(System.Exception)
            {
                System.Console.Write("Error");
            }
    }
    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}