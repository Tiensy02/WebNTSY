import MISAAxios from "axios";
import "regenerator-runtime/runtime";

class BaseService {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
    this.baseAxios = MISAAxios;
  }

  /**
   * gọi API với 1 endpoint riêng
   * @param {string} url đường dẫn riêng
   * @author ntsy(19-07-2023) 
   */
  endpoint(url) {
    return this.baseUrl + url;
  }

  /**
   * lấy toàn bộ dữ liệu
   * @returns danh sách bản ghi
   */
  async getAll() {
      const url = this.baseUrl;
      const res = await this.baseAxios.get(url)
      return res
  }

  /**
   * * thực hiện thêm mới dữ liệu
   * @param {Object} dataAdd đối tượng được thêm
   */
  async post(dataAdd) {
      const url = this.baseUrl
      const res = await this.baseAxios.post(url, dataAdd);
      return res
  }

  /**
   * * thực hiện cập nhật dữ liệu của bản ghi
   * @param {Object} dataUpdate đối tượng cần được sửa
   * @author ntsy(19-07-2023)
   */
  async put(dataUpdate) {
      const res = await this.baseAxios.put(this.baseUrl, dataUpdate);
      return res;
  }

  /**
   * xóa 1 bản ghi
   * @param {string} id id của bản ghi dạng guid
   * @author ntsy(19-07-2023)
   */
  async delete(id) {
      const res = await this.baseAxios.delete(this.baseUrl + `/${id}`);
      return res
  }

  /**
   * xóa nhiều bản ghi
   * @param {Array} ids mảng chứa các id của bản ghi
   * @author ntsy(19-07-2023)
   */
  async deleteMultiple(ids) {
      const url = this.endpoint('/Many')
      const res = await this.baseAxios.delete(url, {
        headers: {
          'Content-Type': 'application/json' // Đặt header Content-Type là application/json để chỉ định dữ liệu gửi đi là JSON
        },
        data: ids 
      });
      return res
  }

}

export default BaseService;