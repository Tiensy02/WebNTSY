import BaseService from "./base-service";
class EmulationTitleService extends BaseService {
  constructor() {
    super("https://localhost:7166/api/v1/EmulationTitles");
  };
  /**
 * lấy dữ liệu với phân trang
 * @param {*} page trang hiện tại
 * @param {*} pageSize số lượng bản ghi trong 1 trang
 * @author ntsy (19-07-2023)
 */
  async getWithPaging(page, pageSize) {
    const url = this.endpoint('/Info');
    const params = { page, pageSize };
    const res = await this.baseAxios.get(url, { params })
    return res
  }
  /**
   * thực hiện gọi api tìm kiếm và lọc danh hiệu thi đua
   * @param {Number} page  trang hiện tại
   * @param {Number} pageSize tổng số bản ghi trên 1 trang
   * @param {Object} filterObject đối tượng chứa thông tin tìm kiếm
   * @returns danh sách thi đua hợp lệ
   */
  async getWithFilter(page, pageSize, filterObject) {
    const url = this.endpoint('/Filter');
    const textSearch = filterObject.textSearch
    const emulationAwardRecipient = filterObject.emulationAwardRecipient
    const emulationMovementType = filterObject.emulationMovementType
    const emulationStatus = filterObject.emulationStatus
    const awardID = filterObject.awardID
    const params = { page, pageSize, textSearch, emulationAwardRecipient, emulationMovementType, emulationStatus, awardID }
    const res = await this.baseAxios.get(url, { params })
    return res
  }
  /**
   * gọi api thực hiện cập nhập trạng thái của nhiều danh hiệu thi đua
   * @param {Array} ids danh sách id của những danh hiệu thi đua được chọn
   * @param {int} emulationStatus trạng thái cần được cập nhập
   * @returns status code của api
   */
  async UpdateManyEmulationTitleStatus(emulationIDs, emulationStatus) {
      const url = this.endpoint('/ManyUpdate');
      const res = await this.baseAxios.put(url, {emulationIDs,emulationStatus});
      return res
  }
}
export default EmulationTitleService;