using AutoMapper;
using InternShip.VideoArchive.Contracts.Abstractions.Data;
using InternShip.VideoArchive.Contracts.Abstractions.Integrations;
using InternShip.VideoArchive.Contracts.Models.Home;
using InternShip.VideoArchive.Implementations.FilmServices;
using Moq;
using Xunit;

namespace InternShip.VideoArchive.Tests
{
	/// <summary>
	/// Тесты для <see cref="HomeFilmHandler">
	/// </summary>
	public class HomeFilmHandlerTests
	{
		private readonly Mock<IFakeDatabaseService> _fakeDatabaseService = new Mock<IFakeDatabaseService>();
		private readonly Mock<IFakeInegrationService> _fakeIntegrationService = new Mock<IFakeInegrationService>();
		private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

		private readonly HomeFilmHandler _homeFilmHandler;

		public HomeFilmHandlerTests()
		{
			_fakeDatabaseService
				.Setup(service => service
					.GetAllFilms())
				.ReturnsAsync(new List<HomeFilm>());

			_homeFilmHandler = new HomeFilmHandler(
				_fakeDatabaseService.Object,
				_fakeIntegrationService.Object,
				_mapper.Object);
		}

		/// <summary>
		/// Если из БД вернулся null, проверяем на исключение
		/// </summary>
		[Fact]
		public void NoFilmsInDatabase_ExceptionReturns()
		{
			Assert.Throws<NullReferenceException>(() => { _homeFilmHandler.IndicateFavouritesOfOurFilms(); });
		}
	}
}