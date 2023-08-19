 const MISAEnum = {
    EmulationAwardRecipient:{
        Individual : 1, // cá nhân
        Collective : 2, // tập thể
        Family:3,// gia đình 
        IndividualAndCollective : 0 // cá nhân và tập thể
    },
    EmulationMovementType:{
        Frequent : 1, // thường xuyên
        Batch : 2, // theo đợt 
        FrequentAndBatch : 0 // thường xuyên và theo đợt
    },
    EmulationStatus:{
        Active : 1, // sử dụng
        NoneActive: 0 //  ngừng sử dụng
    },
    EmulationTitleLever:{
        System:1, // danh hiệu thi đua hệ thống
        Normal:0 // danh hiệu thi đua bình thường
    },
    Action:{
        Delete:10, // xóa
        Export:1, // nhập khẩu
        Import:2, // xuất khẩu
    } 
    
}
export default MISAEnum;
