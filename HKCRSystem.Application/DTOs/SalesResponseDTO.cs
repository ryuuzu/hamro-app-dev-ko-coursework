﻿namespace HKCRSystem.Application.DTOs;

public class SalesResponseDTO
{
    public string Customer { get; set; }
    public string SalesHandledBy { get; set; }
    public string CarName { get; set; }
    public double TotalPrice { get; set; }
    public string PaymentType { get; set; }
    public DateTime SalesDate { get; set; }
    public bool IsPaid { get; set; }
}