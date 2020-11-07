using AutoMapper;
using MedicineTrackingSystem.API.Controllers;
using MedicineTrackingSystem.API.Models.Request;
using MedicineTrackingSystem.API.Models.Response;
using MedicineTrackingSystem.Domain;
using MedicineTrackingSystem.Domain.Constants;
using MedicineTrackingSystem.Domain.Contracts;
using MedicineTrackingSystem.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace MedicineTrackingSystem.API.Test.Controllers
{
    public class MedicineControllerTest
    {
        private Mock<IMedicineService> medicineService;
        public MedicineController medicineController;
        private Mock<ILogger<BaseController>> logger;

        public MedicineControllerTest()
        {
            medicineService = new Mock<IMedicineService>();
            logger = new Mock<ILogger<BaseController>>();
        }

        private void Given_MedicineController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NotesEditRequest, MedicineDto>();
                cfg.CreateMap<MedicineDto, MedicineListResponse>();
            });

            var mapper = config.CreateMapper();
            medicineController = new MedicineController(medicineService.Object, logger.Object, mapper);
            medicineController.ControllerContext = new ControllerContext();
            medicineController.ControllerContext.ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor();
            medicineController.ControllerContext.ActionDescriptor.ActionName = "";
            medicineController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        private IEnumerable<MedicineDto> MedicineList()
        {
            return new List<MedicineDto>()
            {
                new MedicineDto(){Name="Test1", Brand ="Test", MedicineGuid = Guid.NewGuid()},
                new MedicineDto(){Name="Test2", Brand ="Test", MedicineGuid = Guid.NewGuid()},

            };
        }

        private void When_GetMedicines()
        {
            medicineService.Setup(a => a.GetAllMedicines())
                .Returns(Task.FromResult(MedicineList()));
        }


        private void When_AddMedicine()
        {
            medicineService.Setup(a => a.AddMedicine(It.IsAny<MedicineEditDto>()))
                .Returns(Task.FromResult(ServiceResponse.Success(new MedicineDto(), ResponseCodes.Created)));
        }
        private async Task Then_GetMedicines()
        {
            var result = await medicineController.GetAllMedicines();
            Assert.True(((result as ObjectResult).Value as IEnumerable<MedicineListResponse>).Count() > 0);
        }

        private async Task Then_AddMedicine()
        {
            var response = await medicineController.AddMedicine(new MedicineEditDto());

            Assert.Equal(StatusCodes.Status201Created, (response as ObjectResult).StatusCode);
        }

        private void When_UpdateMedicineNotFound()
        {
            medicineService.Setup(a => a.UpdateMedicine(It.IsAny<MedicineEditDto>()))
                .Returns(Task.FromResult(ServiceResponse.Failure("Medicine does not exist.", ResponseCodes.NotFound)));
        }

        private async Task Then_UpdateMedicineNotFound()
        {
            var response = await medicineController.UpdateMedicine(new MedicineEditDto());
            Assert.Equal(StatusCodes.Status404NotFound, (response as ObjectResult).StatusCode);
        }

        [Fact]
        public void VerifyGetAllMedicinesTest() => this.Given(_ => _.Given_MedicineController(), "Given a Controller")
                                    .When(_ => _.When_GetMedicines(), "When we get medicines")
                                    .Then(_ => _.Then_GetMedicines(), "Then get medicines")
                                    .BDDfy();

        [Fact]
        public void VerifyAddMedicineTest() => this.Given(_ => _.Given_MedicineController(), "Given a Controller")
                                    .When(_ => _.When_AddMedicine(), "When we save medicine")
                                    .Then(_ => _.Then_AddMedicine(), "Then medicine got saved")
                                    .BDDfy();
        [Fact]
        public void VerifyUpdateMedicineNotFoundTest() => this.Given(_ => _.Given_MedicineController(), "Given a Controller")
                                    .When(_ => _.When_UpdateMedicineNotFound(), "When we update Medicine who does not exist")
                                    .Then(_ => _.Then_UpdateMedicineNotFound(), "Then got not found error")
                                    .BDDfy();
    }
}
