using AutoMapper;
using NTSY.CevgovApp.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application.Tests
{
    [TestFixture]
    public class EmulationTitleServiceTest
    {
        // Khai báo các đối tượng cần dùng cho unit test
        public IEmulationTitleRepository emulationTitleRepository { get; set; }
        public IMapper mapper { get; set; }
        public IEmulationTitleManager emulationTitleManager { get; set; }

        [SetUp]
        public void SetUp()
        {
            // Tạo các đối tượng giả lập bằng NSubstitute để sử dụng trong unit test
            emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();
            mapper = Substitute.For<IMapper>();
            emulationTitleManager = Substitute.For<IEmulationTitleManager>();
        }

        /// <summary>
        /// test khi list  id trống thì bắn ra ngoại lệ
        /// </summary>
        [Test]
        public void DeleteMultiAsync_EmptyList_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>();
            var expectedMessage = "không được truyền danh sách rỗng";
            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper, emulationTitleManager);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await emulationTitleService.DeleteMultiAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
        /// <summary>
        /// tets khi tìm được ít phần tử hơn số lượng id đã cho ban đầu bắn ra lỗi
        /// </summary>
        [Test]

        public void DeleteMultiAsync_ExistList_ThrowException()
        {
            // Arrange
            // Tạo 1 list gồm 10 id
            List<Guid> ids = new List<Guid>();
            for (int i = 0; i < 10; i++)
            {
                Guid newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var emulationTitles = new List<EmulationTitle>();
            // Tạo list gồm 9 danh hiệu thi đua
            for (int i = 0; i < 9; i++)
            {
                var emulationTitle = new EmulationTitle();
                emulationTitles.Add(emulationTitle);
            }

            emulationTitleRepository.GetListByIdAsync(ids).Returns((IEnumerable<EmulationTitleModel>)emulationTitles);

            var expectedMessage = "không thể xóa";
            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper, emulationTitleManager);

            // Act & Assert 
            var exception = Assert.ThrowsAsync<Exception>(async () => await emulationTitleService.DeleteMultiAsync(ids));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
        /// <summary>
        /// tets khi tìm được đúng số lượng phần tử mà danh sách id có thì thành công
        /// </summary>

        [Test]
        public void DeleteMultiAsync_ExistList_Success()
        {
            // Arrange
            // Tạo 1 list gồm 10 id
            List<Guid> ids = new List<Guid>();
            for (int i = 0; i < 10; i++)
            {
                Guid newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var emulationTitles = new List<EmulationTitle>();
            // Tạo list gồm 10 danh hiệu thi đua
            for (int i = 0; i < 10; i++)
            {
                var emulationTitle = new EmulationTitle();
                emulationTitles.Add(emulationTitle);
            }

            emulationTitleRepository.GetListByIdAsync(ids).Returns((IEnumerable<EmulationTitleModel>)emulationTitles);

            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper, emulationTitleManager);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await emulationTitleService.DeleteMultiAsync(ids));
        }
    }
}
