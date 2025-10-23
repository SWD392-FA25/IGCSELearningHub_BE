using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Analytics
{
    public class KpiSummaryDto
    {
        public decimal RevenuePaid { get; set; }         // Tổng doanh thu (đã thanh toán)
        public int OrdersPaid { get; set; }              // Số đơn đã thanh toán
        public int NewUsers { get; set; }                // Số user mới
        public int NewEnrollments { get; set; }          // Số ghi danh mới
        public int LivestreamRegistrations { get; set; } // Số đăng ký livestream (Paid/All tùy chọn)
        public decimal ARPU { get; set; }                // Doanh thu trung bình / user
    }
}
