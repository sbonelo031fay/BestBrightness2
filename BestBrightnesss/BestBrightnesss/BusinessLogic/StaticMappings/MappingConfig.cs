using AutoMapper;
using BestBrightnesss.DataLogic.Invetory;
using BestBrightnesss.DataLogic.Profile;
using BestBrightnesss.DataLogic.Reports;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.StaticMappings
{
    public class MappingConfig
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RecordSales, SalesView>().ReverseMap();
                cfg.CreateMap<Invetory, InvetoryView>().ReverseMap();
                cfg.CreateMap<Register, RegisterView>().ReverseMap();
                cfg.CreateMap<UserRole, UserRoleView>().ReverseMap();
                cfg.CreateMap<DailySalesReports, DailySalesReportView>().ReverseMap();
                cfg.CreateMap<DailySalesReports, DailySalesReportView>();
                cfg.CreateMap<InvetoryReport, InventoryReportView>();

                // Map SalesView to RecordSales
                cfg.CreateMap<SalesView, RecordSales>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

                // Map SalesView.ProductSaleItem to RecordSales.ProductSaleItem
                cfg.CreateMap<SalesView.ProductSaleItem, RecordSales.ProductSaleItem>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
}
