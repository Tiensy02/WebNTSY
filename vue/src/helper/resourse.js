const MResource = {
    VN:{
        InvalidError:{
            Empty:"không được để trống.",
            NotExist: (label)=>{
                return `${label} không đúng.Vui lòng chọn ${label} trong danh sách.`
            },  
        }, 
        ToolTip:{ 
            Close: "Đóng",
            Guid:"Hướng dẫn",
            SaveAndAdd:"Ctrl + Shift + S",
            Save:"Ctrl + S"
        },
        EmulationTitleNames:{
            EmulationTitleName:"Tên danh hiệu thi đua",
            EmulationCode:"Mã danh hiệu",
            EmulationAwardRecipient:"Đối tượng khen thưởng",
            AwardName:"Cấp khen thưởng",
            EmulationMovementType:"Loại phong trào áp dụng",
            EmulationStatus:"Trạng thái"

        },
        Resource:{
            HeaderName: "Danh hiệu thi đua"
        },
        Tabel:{
            StatusColumn:"Trạng thái"
        },
        EmulationAwardRecipient:{
            Individual: "Cá nhân",
            Collective:"Tập thể",
            Family:"Gia đình",
            IndividualAndCollective: "Cá nhân và tập thể"
        },
        EmulationMovementType:{
            Frequent:"Thường xuyên",
            Batch:"Theo đợt",
            FrequentAndBatch:"Thường xuyên; Theo đợt"
        },
        EmulationStatus:{
            Active:"Sử dụng",
            NoneActive:"Ngừng sử dụng"
        },
        FormName:{
            AddEmulationForm: "Thêm danh hiệu thi đua",
            UpdatedEmulationForm: "Sửa danh hiệu thi đua"
        },
        Active:{
            Delete:"Xóa",
            Export:"Xuất khẩu",
            Import:"Nhập khẩu",
            Press:"Nhập mã hoặc tên danh hiệu...",
            Check:"Đã chọn",
            UnCheck:"Bỏ chọn",
            DeleteEmulation:"Xóa danh hiệu thi đua",
            DeleteSuccess:"Xóa thành công",
            SaveSuccess:"Lưu thành công",
            Notifi:"Thông báo",
            Error:"Lỗi hệ thống vui lòng liên hệ MISA để được giải quyết",
            InputEmulationName:"Nhập tên danh hiệu thi đua",
            InputemulationCode:"Nhập mã danh hiệu thi đua",
            Note:"Ghi chú",
            Cancel:"Hủy",
            SaveAndCreate:"Lưu & thêm mới",
            Save:"Lưu",
            SaveUptade:"Lưu thay đổi",
            InputNote:"Nhập ghi chú",
            Filter:"Lọc danh hiệu",
            Apply:"Áp dụng",
            Use:"Sử dụng",
            UnUse:"Ngừng sử dụng",

            DialogSameCode:(emulationCode)=> {
                return `Mã danh hiệu thi đua <strong>${emulationCode}</strong> đã tồn tại trong danh sách. Xin vui lòng kiểm tra lại.`
            },
            DialogDelete:(emulationCode)=> {
                return `Bạn có chắc chắn muốn xóa Danh hiệu thi đua <strong>${emulationCode}</strong> đã chọn không?`
            },
            DialogDeleteMany:(length)=> {
                return `Xóa <strong>${length} danh hiệu</strong> đã chọn?`
            },
            DialogDeleteFalse:(names) =>{
                return  `Danh hiệu <strong>${names}</strong> là dữ liệu hệ thống, bạn không thể xóa. `
            }

        },
        ElementName:{
            Filter:"Bộ lọc"
        }


    },
    EN:{

    }
}
export default MResource