using System;
using System.Threading.Tasks;
using Camelot.ViewModels.Configuration;
using Camelot.ViewModels.Implementations.MainWindow;
using Camelot.ViewModels.Implementations.MainWindow.FilePanels.Specifications;
using Xunit;

namespace Camelot.ViewModels.Tests
{
    public class SearchViewModelTests
    {
        [Fact]
        public void TestDefaults()
        {
            var configuration = new SearchViewModelConfiguration();
            var viewModel = new SearchViewModel(configuration);

            Assert.False(viewModel.IsSearchEnabled);
            Assert.False(viewModel.IsRegexSearchEnabled);
            Assert.False(viewModel.IsSearchCaseSensitive);
            Assert.Equal(string.Empty, viewModel.SearchText);
        }

        [Fact]
        public void TestShow()
        {
            var configuration = new SearchViewModelConfiguration();
            var viewModel = new SearchViewModel(configuration);
            Assert.False(viewModel.IsSearchEnabled);

            viewModel.Show();
            Assert.True(viewModel.IsSearchEnabled);
        }

        [Theory]
        [InlineData(false, false, typeof(EmptySpecification))]
        [InlineData(false, true, typeof(EmptySpecification))]
        [InlineData(true, true, typeof(NodeNameRegexSpecification))]
        [InlineData(true, false, typeof(NodeNameTextSpecification))]
        public void TestSpecification(bool isSearchEnabled, bool isRegexSearchEnabled,
            Type specificationType)
        {
            var configuration = new SearchViewModelConfiguration();
            var viewModel = new SearchViewModel(configuration)
            {
                IsSearchEnabled = isSearchEnabled,
                IsRegexSearchEnabled = isRegexSearchEnabled
            };

            var specification = viewModel.GetSpecification();

            Assert.IsType(specificationType, specification);
        }

        [Fact]
        public async Task TestSettingsChanged()
        {
            var configuration = new SearchViewModelConfiguration
            {
                TimeoutMs = 10
            };
            var viewModel = new SearchViewModel(configuration);
            var isCallbackCalled = false;
            viewModel.SearchSettingsChanged += (sender, args) => isCallbackCalled = true;

            viewModel.SearchText = "test";

            await Task.Delay(configuration.TimeoutMs * 5);
            Assert.True(isCallbackCalled);
        }
    }
}