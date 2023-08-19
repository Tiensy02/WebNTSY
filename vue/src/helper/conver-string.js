/**
 * @description hàm chuyển đổi giá trị enum thành mô tả
 * @param {String} field tên trường được sử dụng
 * @param {any} value giá trị nhận được
 * @returns 1 chuỗi string mô tả giá trị 
 */
import MResource from "./resourse";
export default function converString(field, value) {
    switch (field) {
        case "EmulationAwardRecipient":
            switch (value) {
                case 1: return MResource['VN'].EmulationAwardRecipient.Individual;
                case 2: return MResource['VN'].EmulationAwardRecipient.Collective;
                case 0: return MResource['VN'].EmulationAwardRecipient.IndividualAndCollective
            } break;
        case "EmulationMovementType":
            switch (value) {
                case 1: return MResource['VN'].EmulationMovementType.Frequent;
                case 2: return MResource['VN'].EmulationMovementType.Batch;
                case 0: return MResource['VN'].EmulationMovementType.FrequentAndBatch
            } break;
        case "EmulationStatus":
            switch (value) {
                case 0: return MResource['VN'].EmulationStatus.NoneActive;
                case 1: return MResource['VN'].EmulationStatus.Active;
            }
        default:
            return value;
    }
}