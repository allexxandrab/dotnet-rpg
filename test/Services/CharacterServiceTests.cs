using AutoFixture;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Mapping;
using dotnet_rpg.Models;
using dotnet_rpg.Repositories;
using dotnet_rpg.Services;
using dotnet_rpg.Services.Implementation;
using Moq;

namespace dotnet_rpg.Tests.Services
{
    public class CharacterServiceTests
    {
        private readonly ICharacterService characterService;
        private readonly Mock<IDbRepository> mockDbRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICharacterMapper> mockCharacterMapper;
        private readonly IFixture fixture;

        public CharacterServiceTests()
        {
            this.mockDbRepository = new Mock<IDbRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockCharacterMapper = new Mock<ICharacterMapper>();

            this.fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            this.characterService = new CharacterService(mockMapper.Object, mockDbRepository.Object, mockCharacterMapper.Object);
        }

        [Fact]
        public async Task AddCharacter_ShouldReturnListWithNewCharacter_WhenSuccessful()
        {
            // Arrange
            var addCharacterRequestDto = fixture.Create<AddCharacterRequestDto>();
            var characterToAdd = fixture.Create<Character>();
            var existingCharacters = fixture.CreateMany<GetCharacterResponseDto>(2).ToList(); 
            var newCharacterResponse = fixture.Create<GetCharacterResponseDto>(); 

            mockMapper.Setup(x => x.Map<Character>(addCharacterRequestDto)).Returns(characterToAdd);

            var expectedCharactersAfterAddition = existingCharacters.Concat(new List<GetCharacterResponseDto> { newCharacterResponse }).ToList();
            mockDbRepository.Setup(x => x.GetCharactersByCurrentUserAsync()).ReturnsAsync(expectedCharactersAfterAddition);

            // Act
            var result = await characterService.AddCharacter(addCharacterRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCharactersAfterAddition.Count, result.Data?.Count); 
            Assert.Contains(newCharacterResponse, result?.Data); 
            mockDbRepository.Verify(x => x.SaveCharacter(characterToAdd), Times.Never);
            mockDbRepository.Verify(x => x.SaveChangesAsync(), Times.Once());
            mockDbRepository.Verify(x => x.GetCharactersByCurrentUserAsync(), Times.Once());
            mockDbRepository.Verify(x => x.GetCurrentUserAsync(), Times.Once());
            mockDbRepository.VerifyNoOtherCalls();
            mockMapper.Verify(x => x.Map<Character>(addCharacterRequestDto), Times.Once());
            mockMapper.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddCharacter_WhenCharacterRequestIsNull_ShouldThrowNullReference()
        {
            // Arrange
            var addCharacterRequestDto = new AddCharacterRequestDto { Name = string.Empty};

            // Act
            Task act() => characterService.AddCharacter(addCharacterRequestDto);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }
    }
}
