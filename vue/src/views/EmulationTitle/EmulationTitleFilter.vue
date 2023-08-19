<template>
    <div class="filter-box" @keyup.enter="tabElementFilter" @keydown.tab="tabElementFilter" ref="filterBox" >
        <div class="filter-box-heading">
            <div class="fileter-title">
                {{ $_MResource['VN'].Active.Filter }}
            </div>
            <div @click="closeFilter" class="icon icon-close"></div>
        </div>
        <div class="filter-box-main">
            <form action="#">
                <div class="form-group">
                    <m-combobox :isEmpty="true" :isLabel="true"
                        :label-text="$_MResource['VN'].EmulationTitleNames.EmulationAwardRecipient"
                        :items="emulationAwardRecipients" :defaultValue="emulationAwardRecipients[0].id"
                        @updateValueCombobox="updateValueCombobox($event, 'emulationAwardRecipient')"
                        @onValidateEmpty="onValidateEmpty" @onValidateExist="onValidateExist"
                        @sendInputComboboxElement="updateElement">
                    </m-combobox>
                </div>
                <div class="form-group">
                    <m-combobox :isEmpty="true" :isLabel="true"
                        :label-text="$_MResource['VN'].EmulationTitleNames.AwardName" :items="rewardlevers" itemID="AwardID"
                        itemName="AwardName" @updateValueCombobox="updateValueCombobox($event, 'awardID')"
                        :defaultValue="rewardlevers[0].AwardID" @onValidateEmpty="onValidateEmpty"
                        @onValidateExist="onValidateExist" @sendInputComboboxElement="updateElement">
                    </m-combobox>
                </div>
                <div class="form-group">
                    <m-combobox :isEmpty="true" :isLabel="true"
                        :label-text="$_MResource['VN'].EmulationTitleNames.EmulationMovementType"
                        :items="emulationMovementTypes" :defaultValue="emulationMovementTypes[0].id"
                        @updateValueCombobox="updateValueCombobox($event, 'emulationMovementType')"
                        @onValidateEmpty="onValidateEmpty" @onValidateExist="onValidateExist"
                        @sendInputComboboxElement="updateElement">
                    </m-combobox>
                </div>
                <div class="form-group">
                    <m-combobox :isEmpty="true" :isLabel="true"
                        :label-text="$_MResource['VN'].EmulationTitleNames.EmulationStatus" :items="emulationStatus"
                        :defaultValue="emulationStatus[0].id"
                        @updateValueCombobox="updateValueCombobox($event, 'emulationStatus')"
                        @onValidateEmpty="onValidateEmpty" @onValidateExist="onValidateExist"
                        @sendInputComboboxElement="updateElement">
                    </m-combobox>
                </div>
            </form>
        </div>
        <div class="filter-box-bottom flex">
            <m-button btn-class="btn--primary" :text-button="$_MResource['VN'].Active.Apply"
                @handlerClickButton="activeFilter" @sendButtonElement="updateElement">
            </m-button>
            <m-button :text-button="$_MResource['VN'].Active.Cancel" @handlerClickButton="closeFilter"
                @sendButtonElement="updateElement">
            </m-button>

        </div>
        <m-dialog v-if="isShowDialog" :dialogMess="errorFilter[0].mess" @closeDialog="closeDialog">
        </m-dialog>
    </div>
</template>

<script>
import AwardService from '../../service/award-service.js'
import tabElement from '../../helper/tabElement.js'
export default {
    name: "emulation-title-filer",
    created() {
        this.getAwards()
    },
    data() {
        return {
            serviceAward: new AwardService(),
            emulationAwardRecipients: [
                {
                    id: null, name: "Tất cả",
                },
                {
                    id: this.$_MEnum.EmulationAwardRecipient.Individual, name: this.$_MResource['VN'].EmulationAwardRecipient.Individual
                },
                {
                    id: this.$_MEnum.EmulationAwardRecipient.Collective, name: this.$_MResource['VN'].EmulationAwardRecipient.Collective
                },
                {
                    id: this.$_MEnum.EmulationAwardRecipient.Family, name: this.$_MResource['VN'].EmulationAwardRecipient.Family
                },
                {
                    id: this.$_MEnum.EmulationAwardRecipient.IndividualAndCollective, name: this.$_MResource['VN'].EmulationAwardRecipient.IndividualAndCollective
                },
            ],

            rewardlevers: [{ AwardID: null, AwardName: "Tất cả" }],

            emulationMovementTypes: [
                {
                    id: null, name: "Tất cả",
                },
                {
                    id: this.$_MEnum.EmulationMovementType.Frequent, name: this.$_MResource['VN'].EmulationMovementType.Frequent,
                },
                {
                    id: this.$_MEnum.EmulationMovementType.Batch, name: this.$_MResource['VN'].EmulationMovementType.Batch,
                }
            ],
            emulationStatus: [
                {
                    id: null, name: "Tất cả",
                },
                {
                    id: this.$_MEnum.EmulationStatus.Active, name: this.$_MResource['VN'].EmulationStatus.Active,
                },
                {
                    id: this.$_MEnum.EmulationStatus.NoneActive, name: this.$_MResource['VN'].EmulationStatus.NoneActive,
                }
            ],
            // các chỉ số cần thiết gom thành 1 đối tượng 
            filterTrophies: {
                emulationAwardRecipient: '',
                emulationMovementType: '',
                emulationStatus: '',
                awardID: '',
            },
            errorFilter: [],
            isShowDialog: false,
            filterElements: []
        }
    },
    methods: {
        /**
         * @description thực hiện đóng bảng filter khi click ra ngoài
         * @param {Object} e event của sự kiện click  
         */
        clickOutSideFilter(e) {
            try {
                console.log(this.$refs.filterBox)
                if (this.$refs.filterBox) {
                    if (!this.$refs.filterBox.contains(e.target)) {
                        this.closeFilter()
                    }
                }
            } catch (err) {
                console.log(err.message)
            }
        },
      
        /** 
         * @description : lấy danh sách thi đua từ api 
         * 
         */
        getAwards() {
            try {
                this.serviceAward
                    .getAll()
                    .then((res) => {
                        this.rewardlevers = this.rewardlevers.concat(res.data)
                    })

            } catch (e) {
                console.log("lỗi lấy api get Award ~" + e)
            }
        },
        /**
         * @description : đóng bảng filter
         * @param(any)
         */
        closeFilter() {
            this.$emit('closeFilter')
            window.removeEventListener("click", this.clickOutSideFilter)
        },
        /**
         * @description: cập nhập giá trị của input đồng thời xoá thành phần lỗi ra khỏi errorFilter
         * @param {Object} dataInput object của MCombobox gửi gồm "name": ô input hiện thời , "newValue": giá trị của ô input
         * @param {String} instanceUpdate trường được cập nhập dữ liệu của filterTrophies
         */
        updateValueCombobox(dataInput, instanceUpdate) {
            this.filterTrophies[instanceUpdate] = dataInput.newValue;
            this.errorFilter = this.errorFilter.filter(elem => {
                return elem.name != dataInput.name
            })

        },
        /**
         * @description xử lí validate
         * @param {Object} errorData object của MCombobox gửi cho để xử lí gồm "name" : thành phần input bị lỗi và "mess": thông báo lỗi
         */
        onValidateEmpty(errorData) {
            if (!this.errorFilter.includes(errorData)) {
                this.errorFilter.push(errorData)
            }
        },
        onValidateExist(errorData) {
            if (!this.errorFilter.includes(errorData)) {
                this.errorFilter.push(errorData)
            }
        },
        /**
         * @description : thực hiện lọc khi không có validate
         */
        activeFilter() {
            if (this.errorFilter.length == 0) {
                this.$emit("filterSucess", this.filterTrophies)
                this.closeFilter();
            } else this.isShowDialog = true;
        },
        /**
         * @description thực hiện đóng dialog
         */
        closeDialog() {
            this.isShowDialog = false
            this.$emit('closeFilter')
        },
        /**
         * @description thực hiện thêm đối tượng vào filterElements
         * @param {Object} element đối tượng được element con gửi cho component cha
         */
        updateElement(element) {
            this.filterElements.push(element)
        },
        tabElementFilter(event) {
            event.preventDefault();
            if (this.errorFilter.length == 0) {
                tabElement(this.filterElements)
            }
        }
    }
}
</script>

<style>
@import url('../../styles/base/filter.css');</style>