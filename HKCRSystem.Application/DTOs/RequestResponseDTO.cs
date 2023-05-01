﻿namespace HKCRSystem.Application.DTOs;

public class RequestResponseDTO
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float Discount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid RequestedCarId { get; set; }
    public string RequestedById { get; set; }
    public string ApprovedById { get; set; }
    public Guid BillingId { get; set; }
}