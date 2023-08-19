import Vue from 'vue'
import Vuex from  'vuex'
import EmulationTitleService from '../service/emulation-title-service'
Vue.use(Vuex)
const store = new Vuex.Store({
    state:{
        pageSize:10,
        totalRecord:1,
        pageCurrent:1,
        emulationTitles:[], // danh sách danh hiệu thi đua theo phân trang
        emulationTitleSelecteds:[], // danh sách những danh hiệu thi đua được chọn
        isFilter:false,
        emulationTitleALl:[], // danh sách gồm tất cả danh hiệu thi đua 
        isSearch:false,
        loadingData:false, // kiểm tra xem có đang load dữ liệu hay không

        emulationTiTleFilterInfo:{
            textSearch:null,
            emulationAwardRecipient:null,
            emulationMovementType:null,
            emulationStatus:null,
            awardID:null
        } //đối tượng chứa những thông tin cần tìm kiếm

    },
    mutations:{
        /**
         * thay đổi pageSie
         * @param {*} state 
         * @param {Number} newPageSize pageSize nhận được khi người dùng clik
         */
        updatePageSize(state, newPageSize) {
            state.pageSize = newPageSize;
        },
        /**
         * thay đổi trang hiện tại
         * @param {*} state 
         * @param {Number} newPageCurrent trang hiện tại
         */
        updatePageCurrent(state, newPageCurrent){
            state.pageCurrent = newPageCurrent;
        },
        // cập nhập giá trạng thái bộ lọc
        updateIsFilter(state,newIsFilter){
            state.isFilter = newIsFilter
        },
        // cập nhập trạng thái tìm kiếm
        updateIsSearch(state,newIsSearch){
            state.isSearch = newIsSearch
        },
        /**
         * cập nhập trạng thái của loading
         * @param {*} state 
         * @param {Boolean} newLoading trạng thái của loading 
         */
        updateLoading(state,newLoading) {
            state.loadingData = newLoading
        },
        
        /**
         * hàm gán giá trị cho mảng lưu trữ danh hiệu thi đua
         * @param {*} state 
         * @param {Array} newEmulationTitle danh sách danh hiệu thi đua
         */
        setEmulationTitle(state, newEmulationTitles){
            state.emulationTitles=newEmulationTitles;
        },
        // gán giá trị cho mới cho danh sách tất cả danh hiệu thi đua
        setEmulationTitleAll(state,newEmulationTitleAll){
            state.emulationTitleALl = newEmulationTitleAll;
        },
       
        /**
         * hàm cập nhập tổng số bản ghi
         * @param {*} state 
         * @param {Number} newTotalRecord tổng số bản ghi
         */
        setTotalRecord(state,newTotalRecord){
            state.totalRecord=newTotalRecord;
        },
      
        /**
         * cập nhập giá trị textSearch trong đối tượng chứa thông tin tìm kiếm
         * @param {*} state 
         * @param {String} valueInput giá trị input
         */
        setEmulationTitleFilterInfoInput(state,valueInput){
            state.emulationTiTleFilterInfo.textSearch = valueInput;
        },

        /**
         * cập nhập cá giá trị bộ lọc cho đối tượng chứa thông tin tìm kiếm
         * @param {*} state 
         * @param {Object} filterTrophies đối tượng chứa những thông tìm kiếm nhận được từ bộ lọc
         */
        setEmulationTitleFilterInfo(state,filterTrophies) {
            state.emulationTiTleFilterInfo.emulationAwardRecipient = filterTrophies.emulationAwardRecipient
            state.emulationTiTleFilterInfo.awardID=filterTrophies.awardID
            state.emulationTiTleFilterInfo.emulationMovementType=filterTrophies.emulationMovementType
            state.emulationTiTleFilterInfo.emulationStatus=filterTrophies.emulationStatus
        },
        /**
         * nếu chưa có trong danh sách thì thêm danh hiệu thi đua được chọn vào danh sách danh hiệu thi đua được chọn 
         * @param {*} state 
         * @param {Object} newEmulationTitle danh hiệu thi đua được chọn 
         */
        addEmulationTitleSelecteds(state,newEmulationTitle){
            if(state.emulationTitleSelecteds) {
                if(state.emulationTitleSelecteds.length == 0){
                    state.emulationTitleSelecteds.push(newEmulationTitle)
                }else{
                    let existEmulationTitle = state.emulationTitleSelecteds.find(element => element.EmulationID == newEmulationTitle.EmulationID) 
                    if( !existEmulationTitle) {
                        state.emulationTitleSelecteds.push(newEmulationTitle)
                    }
                }
            } else state.emulationTitleSelecteds =[newEmulationTitle]
        },
        /**
         * thực hiện xóa danh hiệu thi đua được chọn
         * @param {} state 
         * @param {Object} emulationTitle danh hiệu thi đua được chọn
         */
        removeEmulationTitleSelecteds(state,emulationTitle) {
            state.emulationTitleSelecteds= state.emulationTitleSelecteds.filter(element=> element.EmulationID != emulationTitle.EmulationID)
        },
        /**
         * chọn tất cả các danh hiệu thi đua
         * @param {*} state 
         */
        addAllEmulationTitle(state){
            state.emulationTitleSelecteds=state.emulationTitles
        },
        /**
         * xóa tất cả các danh hiêu thi đua
         * @param {*} state 
         */
        removeAllEmulationTitleSelecteds(state){
            state.emulationTitleSelecteds=[]
        }
    },
    actions:{
        /**
         * hàm thực hiện lấy danh sách danh hiệu thi đua có phân trang và lấy tổng số trang
         * @param {*} commit
         * @param {Object} Object gồm trang hiện tại và số lượng bản ghi trong 1 trang
         */
        async getEmulationTitleAsync({commit}, {pageCurrent, pageSize}){
            try {
                commit("updateLoading",true)
                await new EmulationTitleService() 
                    .getWithPaging(pageCurrent, pageSize)
                    .then((res) => {
                        commit("setEmulationTitle", res.data.EmulationTitles); // thực hiện gán danh sách danh hiệu thi đua
                        commit("setTotalRecord",res.data.TotalRecord) // thực hiện gán tổng số trang
                        
                    })
                    .catch(err=> {
                        console.error("lỗi lấy api của danh hiệu thi đua ~" + err)
                    })
                    commit("updateLoading",false)

            } catch (e) {
                console.error("lỗi lấy api của danh hiệu thi đua ~" + e)
            }
        },
        /**
         * 
         * @param {*} commit
         * @param {Object} Object: gồm trang hiện tại, tổng số trang trên bản ghi và đối tướng chứa thông tin cần tìm kiếm 
         */
        async getEmulationTitleFilterAsync({commit}, {page,pageSize,filterObject}){
            try{ 
                new EmulationTitleService()
                .getWithFilter(page,pageSize,filterObject)
                .then((res)=> {
                    commit("setEmulationTitle",res.data.EmulationTitles)
                    commit("setTotalRecord",res.data.TotalRecord)
                })
                .catch(err=> {
                    console.log("lỗi gọi api tìm kiếm ~ " + err)
                })
            } catch(error) {
                console.log("lỗi gọi api tìm kiếm ~ " + error.message)
            }
        },
        /**
         * hàm lấy toàn bộ danh hiệu thi đua
         */
        async getEmulationTitleAllAsync({commit}) {
                await new EmulationTitleService()
                .getAll()
                .then(res=> {
                    commit("setEmulationTitleAll", res.data)
                })
                .catch(err=> {
                    console.error(err.message)
                })
            
        }
    }
})
export default store