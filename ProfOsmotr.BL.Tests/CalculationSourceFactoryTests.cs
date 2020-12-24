using Moq;
using ProfOsmotr.BL.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace ProfOsmotr.BL.Tests
{
    public class CalculationSourceFactoryTests
    {
        private readonly Mock<IProfessionService> mockProfessionService;
        private readonly CreateProfessionRequest validCreateProfessionRequest;

        public CalculationSourceFactoryTests()
        {
            mockProfessionService = new Mock<IProfessionService>();
            validCreateProfessionRequest = Mock.Of<CreateProfessionRequest>(request =>
                request.NumberOfPersons == 1 &&
                request.NumberOfPersonsOver40 == 1 &&
                request.NumberOfWomen == 0 &&
                request.NumberOfWomenOver40 == 0);
        }

        [Fact]
        public async Task ShouldReturnErrorOnEmptyCreateProfessionRequests()
        {
            CreateCalculationRequest request = GetCreateCalculationRequestWith();

            await AssertReturnsErrorWithCorrectProfessionResponse(request);
        }

        [Fact]
        public async Task ShouldReturnErrorOnNullRequest()
        {
            await AssertReturnsErrorWithCorrectProfessionResponse(null);
        }

        [Fact]
        public async Task ShouldReturnErrorOnBadProfessionResponse()
        {
            // Arrange
            CreateCalculationRequest request = GetCreateCalculationRequestWith(validCreateProfessionRequest);

            var badProfessionServiceResponse = new ProfessionResponse("Profession service error message");
            var professionServiceWithBadResponseMock = new Mock<IProfessionService>();
            professionServiceWithBadResponseMock
                .Setup(profService => profService.CreateProfessionForCalculation(It.IsAny<CreateProfessionRequest>(),
                                                                                 It.IsAny<int>()))
                .Returns(Task.FromResult(badProfessionServiceResponse));

            var factory = new CalculationSourceFactory(professionServiceWithBadResponseMock.Object);

            // Act
            var response = await factory.CreateCalculationSources(request);

            // Assert
            Assert.False(response.Succeed);
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(-1, 0, 0, 0)]
        [InlineData(3, -1, 0, 0)]
        [InlineData(3, 4, 1, 0)]
        [InlineData(3, 2, 4, 0)]
        [InlineData(5, 0, 2, 4)]
        [InlineData(5, 2, 3, 3)]
        public async Task ShouldReturnErrorOnIncorrectNumbers(int numberOfPersons,
                                                              int numberOfPersonsOver40,
                                                              int numberOfWomen,
                                                              int numberOfWomenOver40)
        {
            var invalidProfessionRequest = Mock.Of<CreateProfessionRequest>(request =>
            request.NumberOfPersons == numberOfPersons &&
            request.NumberOfPersonsOver40 == numberOfPersonsOver40 &&
            request.NumberOfWomen == numberOfWomen &&
            request.NumberOfWomenOver40 == numberOfWomenOver40);

            CreateCalculationRequest request = GetCreateCalculationRequestWith(invalidProfessionRequest);

            await AssertReturnsErrorWithCorrectProfessionResponse(request);
        }

        private async Task AssertReturnsErrorWithCorrectProfessionResponse(CreateCalculationRequest invalidRequest)
        {
            // Arrange
            var factory = new CalculationSourceFactory(mockProfessionService.Object);

            // Act
            CalculationSourcesResponse response = await factory.CreateCalculationSources(invalidRequest);

            // Assert
            Assert.False(response.Succeed);
        }

        private CreateCalculationRequest GetCreateCalculationRequestWith(params CreateProfessionRequest[] professionRequests)
        {
            return Mock.Of<CreateCalculationRequest>(request =>
                    request.CreateProfessionRequests == professionRequests);
        }
    }
}