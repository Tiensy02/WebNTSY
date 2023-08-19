using System;
using System.Threading.Tasks;
using NTSY.CevgovApp.Domain;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace NTSY.CevgovApp.Domain.Tests
{
    /// <summary>
    /// Lớp chứa các unit test cho lớp EmulationTitleManager.
    /// </summary>
    [TestFixture]
    public class EmulationTitleTests
    {
        /// <summary>
        /// Test khi mã danh hiệu thi đua đã tồn tại và khác id
        /// hàm CheckEmulationTitleExistByCode sẽ ném ra một ConflictException.
        /// </summary>
        [Test]
        public async Task CheckEmulationTitleExitByCode_EmulationTitleExistsAndDifferentId_ThrowsConflictException()
        {
            // Arrange
            // khởi tạo dữ liệu ban đầu
            var code = "nguyentiensy";
            var id = Guid.NewGuid();
            var existEmultionID = Guid.NewGuid();
            var existingEmulationTitle = new EmulationTitle
            {
                EmulationID = id,
                EmulationCode = code
            };

            // Sử dụng NSubstitute để tạo đối tượng giả lập của IEmulationTitleRepository
            var emulationRitleRepo = Substitute.For<IEmulationTitleRepository>();

            // cho phép hàm GetEmulationTitleByCode ở EmulationtitleRepository trả về đối tượng existingEmulationTitle
            emulationRitleRepo.GetEmulationTitleByCode(code).Returns(existingEmulationTitle);

            var emulationTitleManager = new EmulationTitleManager(emulationRitleRepo);

            // Act & Assert
            // gọi CheckEmulationTitleExistByCode với id khác id của existingEmulationTitle
            Assert.ThrowsAsync<ConflictException>(async () => await emulationTitleManager.CheckEmulationTitleExistByCode(code, existEmultionID));
        }

        /// <summary>
        /// Test khi mã danh hiệu thi đua không tồn tại, hàm CheckEmulationTitleExistByCode không ném ra Exception.
        /// </summary>
        [Test]
        public async Task CheckEmulationTitleExitByCode_WhenEmulationTitleNotExists_DoesNotThrowException()
        {
            // Arrange
            var code = "nguyentiensy002";
            var id = Guid.NewGuid();

            // Sử dụng NSubstitute để tạo đối tượng giả lập của IEmulationTitleRepository
            var emulationRitleRepo = Substitute.For<IEmulationTitleRepository>();

            emulationRitleRepo.GetEmulationTitleByCode(code).ReturnsNull();

            var emulationTitleManager = new EmulationTitleManager(emulationRitleRepo);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await emulationTitleManager.CheckEmulationTitleExistByCode(code, id));
        }
        /// <summary>
        /// Test khi mã danh hiệu thi đua đã tồn tại và trùng id
        /// hàm CheckEmulationTitleExistByCode sẽ không ném ra ngoại lệ.
        /// </summary>
        [Test]
        public async Task CheckEmulationTitleExitByCode_EmulationTitleExistsAndSameId_DoesNotException()
        {
            // Arrange
            // khởi tạo dữ liệu ban đầu
            var code = "nguyentiensy003";
            var id = Guid.NewGuid();
            var existingEmulationTitle = new EmulationTitle
            {
                EmulationID = id,
                EmulationCode = code
            };

            // Sử dụng NSubstitute để tạo đối tượng giả lập của IEmulationTitleRepository
            var emulationRitleRepo = Substitute.For<IEmulationTitleRepository>();

            // cho phép hàm GetEmulationTitleByCode ở EmulationtitleRepository trả về đối tượng existingEmulationTitle
            emulationRitleRepo.GetEmulationTitleByCode(code).Returns(existingEmulationTitle);

            var emulationTitleManager = new EmulationTitleManager(emulationRitleRepo);

            // Act & Assert
            // gọi CheckEmulationTitleExistByCode với id khác id của existingEmulationTitle
            Assert.DoesNotThrowAsync(async () => await emulationTitleManager.CheckEmulationTitleExistByCode(code, id));

        }

    }
}
