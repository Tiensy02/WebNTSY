<template>
    <div class="main-content">
        <div class="content__heading">
            <div class="content__title">
                {{ $_MResource['VN'].Resource.HeaderName }}
            </div>
        </div>
        <div class="table__heading">
            <div class="content__heading__element">
                <text-input nameInput="input-text" :isIcon="true" :textPlaceholder="$_MResource['VN'].Active.Press"
                    nameIcon="icon-search" :default-value="searchText" :isValidate="false" :isSearchInput="true"
                    @searchInput="searchInput">
                </text-input>
                <m-button btnClass="btn-icon" :is-icon="true" :iconName="isFilter ? 'icon-filter-active' : 'icon-filter'"
                    textButton="Bộ lọc" @handlerClickButton="openFilter">
                </m-button> 
                <div v-if="isFilter" @click="unableFilter">
                    <div class="primary-text">{{ $_MResource['VN'].ElementName.Filter }}</div>
                </div>
                <emulation-title-filter v-show="filterShow" @closeFilter="closeFilter" @filterSucess="filterSucess">
                </emulation-title-filter>
            </div>
            <div v-if="emulationTitleSlecteds.length == 0" class="content__heading__element">
                <m-button btnClass="btn-icon btn--primary" :is-icon="true" iconName="icon-add" textButton="Thêm danh hiệu"
                    @handlerClickButton="openForm">
                </m-button>
                <div>
                    <m-button btnClass="btn-just-icon" :is-icon="true" iconName="icon-triggle"
                        :isContextMenu="true" :menuItems="menuButton" @handlerClickMenuInButton="handlerData"></m-button>
                </div>
            </div>
            <div v-else class="content__heading__element">
                <div>{{ $_MResource['VN'].Active.Check }} <b> {{ emulationTitleSlecteds.length }} </b></div>
                <div class="primary-text" @click="changeIsCheckAll">{{ $_MResource['VN'].Active.UnCheck }}</div>
                <m-button btnClass="btn--secondary" :textButton="$_MResource['VN'].Active.Use"
                    @handlerClickButton="updateEmulationTitleStatus($_MEnum.EmulationStatus.Active)">
                </m-button>
                <m-button :textButton="$_MResource['VN'].Active.UnUse"
                    @handlerClickButton="updateEmulationTitleStatus($_MEnum.EmulationStatus.NoneActive)">
                </m-button>
                <m-button btnClass="btn-delete" :textButton="$_MResource['VN'].Active.Delete" @handlerClickButton=preDeleteMany>
                </m-button>

            </div>
        </div>
        <emulation-titles @updateEmulationTitle="updateEmulationTitle" @deleteEmulationSucess="deleteEmulationSucess"
            @deleteEmulation="preDelete" @updateEmulationStatus="updateEmulationTitleStatus"
            :emulationTitleSlecteds="emulationTitleSlecteds" :emulationTitles="emulationTitles"></emulation-titles>
        <emulation-title-form :formName="formName" :emulationTitle="emulationTitleUpdate" v-if="formShow" @closeForm="closeForm"
            @UpdateFormSuccess="UpdateFormSuccess" @UpdateFormFalse="UpdateFormFalse" 
            @handlerException="handlerException" @openForm="openForm"
            :disabled="disabled" >
        </emulation-title-form>
        <m-toast-mess :contentMess="toastMessageContent" :isAppear="hasToastMess" @hiddenToastMess="hiddenToastMess">
        </m-toast-mess>
        <m-dialog v-if="isShowDialog" :hasManyButton="hasManyButtoninDialog" :dialogMess="dialogMess"
            :dialogTitle="dialogTitle" textButtonSecondary="Không" textButtonPrimary="Xóa danh hiệu"
            :handlerDeletel="isHandlerDeletelDialog" @closeDialog="closeDialog" @handlerClickButtonPrimary="deleteMany">
        </m-dialog>
    </div>
</template>

<script>
import EmulationTitleService from '../service/emulation-title-service';
import exportDataToExcel from '../service/export-data-to-excel';
import converString from '../helper/conver-string';
export default {
    name: "theMain",
    created() {
        this.$store.dispatch("getEmulationTitleAsync", {
            pageCurrent: 1,
            pageSize: this.$store.state.pageSize
        });
    },

    data() {
        return {
            formName: this.$_MResource["VN"].FormName.AddEmulationForm,
            formShow: false,
            filterShow: false,
            searchText: "",
            emulationTitleUpdate: {},
            hasToastMess: false,
            toastMessageContent: "",
            isShowDialog: false,
            dialogTitle: "MISA CeGov",
            dialogMess: this.$_MResource['VN'].Active.DeleteEmulation,
            hasManyButtoninDialog: true,
            disabled: false,
            isHandlerDeletelDialog: false,
            emulationTitle: {},
            menuButton: [{ id: this.$_MEnum.Action.Import, name: this.$_MResource['VN'].Active.Import, info: "" },
            { id: this.$_MEnum.Action.Export, name: this.$_MResource['VN'].Active.Export, info: "" }],
        }
    },
    computed: {
        // danh sách danh hiệu thi đua
        emulationTitles() {
            return this.$store.state.emulationTitles
        },
        emulationTitleALl() {
            return this.$store.state.emulationTitleALl
        },
        // danh sách danh hiệu thi đua được chọn
        emulationTitleSlecteds() {
            return this.$store.state.emulationTitleSelecteds
        },
        // số lượng bản ghi trên 1 trang
        pageSize() {
            return this.$store.state.pageSize
        },
        // trang hiện tại
        pageCurrent() {
            return this.$store.state.pageCurrent
        },
        // đối tượng tìm kiếm
        emulationTiTleFilterInfo() {
            return this.$store.state.emulationTiTleFilterInfo
        },
        //trạng thái của bộ lọc
        isFilter() {
            return this.$store.state.isFilter
        },
        // trạng thái ô input
        isSearch() {
            return this.$store.state.isSearch
        }
    },
    methods: {
        /**
         * thực hiện gọi hàm xuất khẩu excel cho danh hiệu thi đua nếu action là hành động xuất khẩu
         * @param {Number} action id của menuContext box
         */
        async handlerData(action) {
            if(action== this.$_MEnum.Action.Export){
                await this.$store.dispatch("getEmulationTitleAllAsync");
                const columnNames = {
                    EmulationName :this.$_MResource['VN'].EmulationTitleNames.EmulationTitleName,
                    EmulationCode: this.$_MResource['VN'].EmulationTitleNames.EmulationCode,
                    EmulationAwardRecipient: this.$_MResource['VN'].EmulationTitleNames.EmulationAwardRecipient,
                    AwardName:this.$_MResource['VN'].EmulationTitleNames.AwardName,
                    EmulationMovementType: this.$_MResource['VN'].EmulationTitleNames.EmulationMovementType,
                    EmulationStatus:this.$_MResource['VN'].EmulationTitleNames.EmulationStatus,
                }
                this.exportFile(columnNames,this.$_MResource['VN'].Resource.HeaderName,this.emulationTitleALl)
            }

        },
        /**
         * @description hàm thực hiện download file excel
         * @param {Object} columnNames đối tượng gồm các thuộc tính cần thiết và đã sắp xếp theo ý muốn theo ý muốn
         * @param {String} title tên tiêu đề của mảng
         * @param {Array} data mảng chứa các đối tượng nhận được từ back end
         */
        exportFile(columnNames, title, data) {
            const dataExport = this.sortByColumnNames(data, columnNames) 
            const excelBlob = exportDataToExcel(dataExport, title);
            const excelBlobUrl = URL.createObjectURL(excelBlob);
            window.open(excelBlobUrl, '_blank');
        },
        /**
         * @description hàm thực hiện sắp xếp lại thứ tự thuộc tính của đối tượng trong data theo ColumnNames
         * @param {Array} array mảng chứa các đối tượng nhận được từ back end
         * @param {Object} columnNames đối tượng gồm các thuộc tính cần thiết và đã sắp xếp theo ý muốn theo ý muốn
         * VD: {emulationName: "danh hiệu thi đua",
         *      emulationCode:"mã danh hiệu thi đua "}
         * @returns {Array} mảng chứa các đối tượng đã được sắp xếp và đổi tên thuộc tính phù hợp để hiển thị
         */
        sortByColumnNames(array, columnNames) {
            try{
                const sortedArray = [];
                array.forEach((obj) => {
                    const sortedObj = {};
                    Object.keys(columnNames).forEach((columnName) => {
                        if (obj.hasOwnProperty(columnName)) {
                            // tạo đối tượng với tên thuộc tính là tên của columnNames và chuyển đổi từ dữ liệu resource 
                            // sang dữ liệu để hiển thị
                            sortedObj[columnNames[columnName]] = converString(columnName,obj[columnName]); 
                        }
                    });
                    sortedArray.push(sortedObj);
                });
                return sortedArray;
            }catch(err) {
                console.log(err);
            }
        },
        

        /** 
         * đóng form
         */
        closeForm() {
            this.formShow = false;
        },
        /**
         * mở form thêm mới danh hiệu thi đua
         */
        openForm() {
            this.disabled = false
            this.formName = this.$_MResource["VN"].FormName.AddEmulationForm
            this.formShow = true;
        },
        /**
         * đóng filter
         */
        closeFilter() {
            this.filterShow = false;
        },
        // mở bộ lọc
        openFilter() {
            this.filterShow = true;
        },
        // đóng dialog
        closeDialog() {
            this.isShowDialog = false;
        },
        /**
         * @description thực hiện bỏ bộ lọc và dữ liệu ô tìm kiếm
         */
        unableFilter() {
            try{
                this.$store.commit("setEmulationTitleFilterInfo", {})
                this.$store.commit("updateIsFilter", false)
                if (this.isSearch) {
                    this.$store.dispatch("getEmulationTitleFilterAsync", {
                        page: this.pageCurrent,
                        pageSize: this.pageSize,
                        filterObject: this.emulationTiTleFilterInfo
                    })
                } else {
                    this.$store.commit("updatePageCurrent", 1)
                    this.$store.dispatch("getEmulationTitleAsync", {
                        pageCurrent: 1,
                        pageSize: this.pageSize
                    });
                }
            } catch(err) {
                console.error(err)
            }
        },
        /**
         * cập nhập đối tượng chứa thông tin tìm kiếm sau đó thực hiện lọc
         * @param {Object } filterTrophies dữ liệu được gửi từ bộ lọc
         */
        filterSucess(filterTrophies) {
            try{
                this.$store.commit("setEmulationTitleFilterInfo", filterTrophies)
                this.$store.commit("updateIsFilter", true)
                this.$store.dispatch("getEmulationTitleFilterAsync", {
                    page: this.pageCurrent,
                    pageSize: this.pageSize,
                    filterObject: this.emulationTiTleFilterInfo
                })
            } catch(err) {
                console.error(err)
            }

        },
        /**
         * sử lí khi xóa thành công
         */
        deleteEmulationSucess() {
            this.toastMessageContent = this.$_MResource['VN'].Active.DeleteSuccess
            this.hasToastMess = true;
        },
        /**
         * cập nhập giá trị của đối tượng chứa thông tin tìm kiếm, sau đó gọi api tìm kiếm
         * @param {String} valueInput giá trị của input
         */
        searchInput(valueInput) {
            try{
                if (!valueInput.trim() == "") {
                    this.$store.commit("setEmulationTitleFilterInfoInput", valueInput);
                    this.$store.commit("updateIsSearch", true)
                    this.$store.commit("updatePageCurrent", 1)
                    this.$store.dispatch("getEmulationTitleFilterAsync", {
                        page: this.pageCurrent,
                        pageSize: this.pageSize,
                        filterObject: this.emulationTiTleFilterInfo
                    })
                } else {
                    this.$store.commit("updateIsSearch", false)
                    this.unableFilter();
                }
            }catch(err){
                console.error(err)
            }
        },
        /**
         * gán giá trị danh sách thi đua được chọn và mở form chỉnh sửa danh sách thi đua
         * @param {Object} record bản ghi được chọn để chỉnh sửa
         */
        updateEmulationTitle(record) {
            try{
                this.emulationTitleUpdate = record;
                // nếu là dữ liệu hệ thống thì bật disable 
                if (record.EmulationTitleLever == this.$_MEnum.EmulationTitleLever.System) {
                    this.disabled = true
                } else {
                    this.disabled = false
                }
                this.formName = this.$_MResource["VN"].FormName.UpdatedEmulationForm
                this.formShow = true;
            }catch(err) {
                console.error(err)
            }
        },
        /**
         * thực hiện đóng toast message
         */
        hiddenToastMess() {
            this.hasToastMess = false;
        },
        /**
         * @description hàm thực hiện khi thêm mới danh hiệu thi đua thành công
         */
        async UpdateFormSuccess(isSaveAndCreate) {
            try {
                this.toastMessageContent = this.$_MResource['VN'].Active.SaveSuccess
                this.hasToastMess = true;
                this.$store.commit("updatePageCurrent", 1)
                await this.$store.dispatch("getEmulationTitleAsync", {
                    pageCurrent: 1,
                    pageSize: this.$store.state.pageSize
                });
                if(isSaveAndCreate) this.openForm()
            } catch(err) {
                console.error(err);
            }
        },
        /**
         * @description thực hiện hiện dialog thông báo khi thêm mới hoặc sửa danh hiệu bị trùng mã 
         * @param {String} emulationCode mã danh hiệu thi đua bị trùng
         */
        UpdateFormFalse(emulationCode) {
                this.dialogMess = this.$_MResource['VN'].Active.DialogSameCode(emulationCode);
                this.hasManyButtoninDialog = false
                this.isShowDialog = true
            
        },
        // thực hiện xóa tất cả các danh hiệu thi đua được chọn
        changeIsCheckAll() {
            this.$store.commit("removeAllEmulationTitleSelecteds")
        },
        /**
         * thực hiện cập trạng thái của danh hiệu thi đua của nhiều danh hiệu thi đua được chọn
         * @param {Number} emulationStatus trạng thái của danh hiệu thi đua
         * 
         */
        updateEmulationTitleStatus(emulationStatus, eumlationID) {
            try{
                let emulationTitleIDs = [];
                this.emulationTitleSlecteds.forEach(element => {
                    emulationTitleIDs.push(element.EmulationID)
                })
                if (eumlationID != null) {
                    emulationTitleIDs.push(eumlationID)
                }
                this.$store.commit("updatePageCurrent", 1)
                new EmulationTitleService()
                    .UpdateManyEmulationTitleStatus(emulationTitleIDs, emulationStatus)
                    .then(res => {
                        this.$store.dispatch("getEmulationTitleAsync", {
                            pageCurrent: 1,
                            pageSize: this.$store.state.pageSize
                        });
                        this.$store.commit("removeAllEmulationTitleSelecteds")
                        this.toastMessageContent = this.$_MResource['VN'].Active.SaveSuccess
                        this.hasToastMess = true;
                    })
                    .catch(err => {
                        this.handlerException(err.response.data.UserMess)
                    })
            } catch(err) {
                console.error(err)
            }
        },
        /**
         * @description thực hiện xóa những danh hiệu thi đua được chọn
         */
        deleteMany() {
            try{
                let emulationTitleIDs = [];
                this.emulationTitleSlecteds.forEach(element => {
                    emulationTitleIDs.push(element.EmulationID)
                })
                //this.emulationTitle được cập nhập từ component competitionList , nếu chưa cập nhập thì this.emulationTitle rỗng nên sẽ không thêm vào danh sách được xóa
                if (this.emulationTitle.EmulationID) {
                    emulationTitleIDs.push(this.emulationTitle.EmulationID)
                }
                this.$store.commit("updatePageCurrent", 1)
                new EmulationTitleService()
                    .deleteMultiple(emulationTitleIDs)
                    .then(res => {
                        this.$store.dispatch("getEmulationTitleAsync", {
                            pageCurrent: 1,
                            pageSize: this.$store.state.pageSize
                        });
                        this.deleteEmulationSucess()
                        this.$store.commit("removeAllEmulationTitleSelecteds")
                        this.closeDialog()
                    })
                    .catch(err => {
                        if (err.response.data.ErrorCode == 403) {
                            console.log(err.response.data)
                            let emulationNames = err.response.data.EmulationTitleNames
                            this.dialogTitle = this.$_MResource['VN'].Active.Notifi
                            this.dialogMess = this.$_MResource['VN'].Active.DialogDeleteFalse(emulationNames)
                            this.hasManyButtoninDialog = false
                            this.isHandlerDeletelDialog = true
                            this.isShowDialog = true
                        } else {
                            this.handlerException(err.response.data.UserMess)
                        }
                    })
            }catch(err) {
                console.error(err)
            }

        },

        /**
         *  @description thực hiện gọi dialog xóa 1 danh hiện thi đua
         * @param {Object} emulationTitle danh hiệu thi đua đang được thao tác 
         */
        preDelete(emulationTitle) {
            this.emulationTitle = emulationTitle;
            this.dialogTitle = this.$_MResource['VN'].Active.DeleteEmulation
            this.dialogMess = this.$_MResource['VN'].Active.DialogDelete(emulationTitle.EmulationCode)
            this.hasManyButtoninDialog = true
            this.isShowDialog = true
        },
        /**
         * @description thực hiện gọi dialog xóa nhiều danh hiệu thi đua
         */
        preDeleteMany() {
            this.dialogTitle = this.$_MResource['VN'].Active.DeleteEmulation
            this.dialogMess = this.$_MResource['VN'].Active.DialogDeleteMany(this.emulationTitleSlecteds.length)
            this.hasManyButtoninDialog = true
            this.isShowDialog = true
        },
        /**
         * hàm xử lí chung khi có api bị lỗi
         * @param {String} message thông báo lỗi
         */
        handlerException(message) {
            if (message) {
                this.dialogMess = message
            } else {
                this.dialogMess = this.$_MResource['VN'].Active.Error
            }
            this.dialogTitle = this.$_MResource['VN'].Active.Notifi
            this.hasManyButtoninDialog = false
            this.isShowDialog = true

        },
       

    },


}
</script>

<style>
@import url('../styles/layout/containt-main.css');
</style>