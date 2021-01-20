using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProfOsmotr.BL.Tests
{
    public class Calculator302nTests
    {
        [Fact]
        public void ShouldCompanyCalculationBeCorrect()
        {
            // Arrange

            var calculator = new Calculator302n();

            var orderExamination1 = CreateOrderExamination(1, 100);
            var orderExamination2 = CreateOrderExamination(2, 119);
            var orderExamination3 = CreateOrderExamination(3, 123, TargetGroupId.ForWomenOnly);
            var orderExamination4 = CreateOrderExamination(4, 142, TargetGroupId.ForPersonsOver40Only);

            var orderItem1 = CreateOrderItemWith(orderExamination1, orderExamination2);
            var orderItem2 = CreateOrderItemWith(orderExamination3, orderExamination4);
            var orderItem3 = CreateOrderItemWith(orderExamination1, orderExamination3);

            var calcSource1 = new CalculationSource()
            {
                NumberOfPersons = 15,
                NumberOfWomen = 12,
                NumberOfPersonsOver40 = 12,
                NumberOfWomenOver40 = 1,
                Profession = CreateProfessionWith(orderItem1, orderItem3)
            };
            var calcSource2 = new CalculationSource()
            {
                NumberOfPersons = 27,
                NumberOfWomen = 8,
                NumberOfPersonsOver40 = 17,
                NumberOfWomenOver40 = 5,
                Profession = CreateProfessionWith(orderItem2, orderItem3)
            };

            // Act

            IEnumerable<CalculationResultItem> results = calculator.CalculateResult(new[] { calcSource1, calcSource2 });

            // Assert

            Assert.Equal(4, results.Count());
            Assert.Equal(10859, GetTotalSum(results));
        }

        [Fact]
        public void ShouldSingleCalculationBeCorrect()
        {
            // Arrange

            var calculator = new Calculator302n();

            var orderExamination1 = CreateOrderExamination(1, 100);
            var orderExamination2 = CreateOrderExamination(2, 111);
            var orderExamination3 = CreateOrderExamination(3, 107);

            var orderItem = CreateOrderItemWith(orderExamination1, orderExamination2, orderExamination3);

            var calcSource = new CalculationSource()
            {
                NumberOfPersons = 1,
                Profession = CreateProfessionWith(orderItem)
            };

            // Act

            IEnumerable<CalculationResultItem> results = calculator.CalculateResult(new[] { calcSource });

            // Assert

            Assert.Equal(3, results.Count());
            Assert.DoesNotContain(results, item => item.Amount != 1);
            Assert.Equal(318, GetTotalSum(results));
        }

        [Fact]
        public void ShouldTargetGroupsResolvesCorrectly()
        {
            // Arrange

            var calculator = new Calculator302n();

            var orderExaminationForWomenOnly = CreateOrderExamination(1, 101, TargetGroupId.ForWomenOnly);
            var orderExaminationForPersonsOver40Only = CreateOrderExamination(2, 119, TargetGroupId.ForPersonsOver40Only);
            var orderExaminationForWomenOver40Only = CreateOrderExamination(3, 109, TargetGroupId.ForWomenOver40Only);

            var orderItem = CreateOrderItemWith(orderExaminationForWomenOnly,
                                                orderExaminationForPersonsOver40Only,
                                                orderExaminationForWomenOver40Only);

            var calcSource = new CalculationSource()
            {
                NumberOfPersons = 10,
                NumberOfWomen = 9,
                NumberOfPersonsOver40 = 4,
                NumberOfWomenOver40 = 0,
                Profession = CreateProfessionWith(orderItem)
            };

            // Act

            IEnumerable<CalculationResultItem> results = calculator.CalculateResult(new[] { calcSource });

            // Assert

            Assert.DoesNotContain(results, item => item.Service.OrderExamination.TargetGroupId == TargetGroupId.ForAll);
            Assert.DoesNotContain(results, item => item.Service.OrderExamination.TargetGroupId == TargetGroupId.ForWomenOver40Only);
            Assert.Equal(2, results.Count());
            Assert.Equal(1385, GetTotalSum(results));
        }

        [Fact]
        public void TheSameServicesShouldntBeCountedTwice()
        {
            // Arrange

            var calculator = new Calculator302n();

            var orderExamination = CreateOrderExamination(1, 100);

            var orderItem1 = CreateOrderItemWith(orderExamination);
            var orderItem2 = CreateOrderItemWith(orderExamination);

            var calcSource = new CalculationSource()
            {
                NumberOfPersons = 1,
                Profession = CreateProfessionWith(orderItem1, orderItem2)
            };

            // Act

            IEnumerable<CalculationResultItem> results = calculator.CalculateResult(new[] { calcSource });

            // Assert

            Assert.Single(results);
            Assert.Equal(100, GetTotalSum(results));
        }

        [Fact]
        public void ThrowsOnNullOrEmptySources()
        {
            var calculator = new Calculator302n();

            Assert.Throws<ArgumentNullException>(() => calculator.CalculateResult(null));
            Assert.Throws<InvalidOperationException>(() => calculator.CalculateResult(new CalculationSource[] { }));
        }

        private OrderExamination CreateOrderExamination(int id, // для исключения повторов услуги сравниваются по id услуги, МО и обследования
                                                        decimal actualServicePrice,
                                                        TargetGroupId targetGroupId = TargetGroupId.ForAll)
        {
            var orderExamination = new OrderExamination()
            {
                TargetGroupId = targetGroupId
            };
            orderExamination.ActualClinicServices.Add(new ActualClinicService()
            {
                Service = new Service()
                {
                    Id = id,
                    OrderExaminationId = id,
                    OrderExamination = orderExamination,
                    Price = actualServicePrice
                }
            });
            return orderExamination;
        }

        private OrderItem CreateOrderItemWith(params OrderExamination[] orderExaminations)
        {
            var orderItem = new OrderItem();
            foreach (var examination in orderExaminations)
            {
                orderItem.OrderExaminations.Add(examination);
            }
            return orderItem;
        }

        private Profession CreateProfessionWith(params OrderItem[] orderItems)
        {
            var profession = new Profession();
            foreach (var item in orderItems)
            {
                profession.OrderItems.Add(item);
            }
            return profession;
        }

        private decimal GetTotalSum(IEnumerable<CalculationResultItem> resultItems)
        {
            return resultItems.Sum(item => item.Service.Price * item.Amount);
        }
    }
}