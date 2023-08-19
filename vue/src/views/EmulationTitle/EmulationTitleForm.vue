<template>
    <div class="modal-overlay" @keyup.enter="tabFormElements" @keydown.tab="tabFormElements">
        <div class="competition-form" ref="EmulationTitleForm">
            <div class="form-header flex">
                <p class="form-title">{{ formName }}</p>
                <div class="form-icon flex">
                    <div class="icon icon-guide tooltip--top"><span class="tooltiptext">{{
                        $_MResource['VN'].ToolTip.Guid }}</span></div>
                    <div @click="closeForm" class="icon icon-close tooltip--top"><span class="tooltiptext">{{
                        $_MResource['VN'].ToolTip.Close }}</span></div>
                </div>
            </div>
            <div class="form-main" ref="formMain">
                <div class="form-group" ref="form1">
                    <text-input :is-label="true" :label-text="$_MResource['VN'].EmulationTitleNames.EmulationTitleName"
                        nameInput="select-input" :isRequest="true"
                        :textPlaceholder="$_MResource['VN'].Active.InputEmulationName"
                        :defaultValue="emulation.EmulationName" @updateValue="updateValue($event, 'EmulationName')"
                        @onValidateInput="onValidateInput" :disabled="disabled" @sendInputElement="updateElement">
                    </text-input>
                </div>
                <div class="group-two flex">
                    <div class="form-group" ref="form2">
                        <text-input :is-label="true" :label-text="$_MResource['VN'].EmulationTitleNames.EmulationCode"
                            nameInput="select-input" :isRequest="true"
                            :maxLength="50"
                            :textPlaceholder="$_MResource['VN'].Active.InputemulationCode"
                            :defaultValue="emulation.EmulationCode" @updateValue="updateValue($event, 'EmulationCode')"
                            @onValidateInput="onValidateInput" @sendInputElement="updateElement" :disabled="disabled">
                        </text-input>
                    </div>
                    <div class="form-group">
                        <selected-input @validateSelectInput="onValidateInput"
                            @updateSelctedInput="updateValue($event, 'EmulationAwardRecipient')"
                            @sendInputElement="updateElement"
                            :labelText="$_MResource['VN'].EmulationTitleNames.EmulationAwardRecipient"
                            :listAbel="emulationAwardRecipients" :defaultValue="emulation.EmulationAwardRecipient"
                            :disabled="disabled">
                        </selected-input>
                    </div>
                </div>
                <div class="group-two flex">
                    <div class="form-group" ref="form3">
                        <m-combobox :isEmpty="false" :isLabel="true"
                            :label-text="$_MResource['VN'].EmulationTitleNames.AwardName" :isRequest="true"
                            :items="rewardlevers" :isIcon="true" itemID="AwardID" itemName="AwardName"
                            :defaultValue="emulation.AwardID" :disabled="disabled" @onValidateEmpty="onValidateInput"
                            @updateValueCombobox="updateValue($event, 'AwardID')" @onValidateExist="onValidateInput"
                            @sendInputComboboxElement="updateElement">
                        </m-combobox>
                    </div>
                    <div class="form-group">
                        <selected-input :labelText="$_MResource['VN'].EmulationTitleNames.EmulationMovementType"
                            :listAbel="emulationMovementTypes" @validateSelectInput="onValidateInput"
                            @sendInputElement="updateElement"
                            @updateSelctedInput="updateValue($event, 'EmulationMovementType')" :disabled="disabled"
                            :defaultValue="emulation.EmulationMovementType">
                        </selected-input>
                    </div>
                </div>
                <div class="form-group">
                    <m-text-area labelText="Ghi chú" :disabled="disabled" :valueText="emulation.EmulationNote"
                        @updateValue="updateValue($event, 'EmulationNote')" @sendTextAreaElement="updateElement">
                    </m-text-area>
                </div>
                <div v-if="formName == this.$_MResource['VN'].FormName.UpdatedEmulationForm" class="form-group">
                    <m-radio-input :labelText="$_MResource['VN'].Active.Note" :items="emulationStatus" nameRadio="Status"
                        :defaultValue="emulation.EmulationStatus" @updateRadioInput="updateValue($event, 'EmulationStatus')"
                        @sendRadioInputElement="updateElement">
                    </m-radio-input>
                </div>
            </div>
            <div class="form-bottom flex" v-if="formName == this.$_MResource['VN'].FormName.AddEmulationForm">
                <m-button :text-button="$_MResource['VN'].Active.Save" btnClass="btn--primary" :hasTooltip="true"
                    :toolTipText="$_MResource['VN'].ToolTip.Save" @sendButtonElement="updateElement"
                    @handlerClickButton="saveEmulation(!isSaveAndCreate)">
                </m-button>
                <m-button :text-button="$_MResource['VN'].Active.SaveAndCreate" btnClass="btn--secondary"
                    @handlerClickButton="saveEmulation(isSaveAndCreate)" @sendButtonElement="updateElement"
                    :hasTooltip="true" :toolTipText="$_MResource['VN'].ToolTip.SaveAndAdd">
                </m-button>
                <div @click="closeForm">
                    <m-button :text-button="$_MResource['VN'].Active.Cancel" @sendButtonElement="updateElement"
                        @handlerClickButton="closeForm">
                    </m-button>
                </div>
            </div>
            <div class="form-bottom flex" v-else>
                <m-button :text-button="$_MResource['VN'].Active.SaveUptade" btnClass="btn--primary"
                    @sendButtonElement="updateElement" @handlerClickButton="saveEmulation(!isSaveAndCreate)"
                    :hasTooltip="true" :toolTipText="$_MResource['VN'].ToolTip.Save">
                </m-button>
                <m-button :text-button="$_MResource['VN'].Active.Cancel" @sendButtonElement="updateElement"
                    @handlerClickButton="closeForm">
                </m-button>
            </div>
        </div>

    </div>
</template>

<script>
import MResource from '../../helper/resourse.js'
import AwardService from '../../service/award-service.js'
import EmulationTitleService from '../../service/emulation-title-service'
import tabElement from '../../helper/tabElement.js'
export default {
    name: "emulation-title-form",
    created() {
        this.getAwards()
    },
    mounted() {
        window.addEventListener("keydown", this.saveByKeyboard)

        // thực hiện focus vào ô đầu tiên khi mở form nếu không bị disabled
        if (!this.disabled) this.onfocusFirstInput()
    },
    beforeDestroy(){
        window.removeEventListener("keydown", this.saveByKeyboard)
    },
    data() {
        return {
            emulation: this.createEmulation(this.formName),
            serviceAward: new AwardService(),
            serviceEmulationTitle: new EmulationTitleService(),
            rewardlevers: [{}],
            emulationAwardRecipients: [
                {
                    id: this.$_MEnum.EmulationAwardRecipient.Individual, name: this.$_MResource['VN'].EmulationAwardRecipient.Individual,
                },
                {
                    id: this.$_MEnum.EmulationAwardRecipient.Collective, name: this.$_MResource['VN'].EmulationAwardRecipient.Collective,
                }
            ],
            emulationMovementTypes: [
                {
                    id: this.$_MEnum.EmulationMovementType.Frequent, name: this.$_MResource['VN'].EmulationMovementType.Frequent,
                },
                {
                    id: this.$_MEnum.EmulationMovementType.Batch, name: this.$_MResource['VN'].EmulationMovementType.Batch,
                }
            ],
            emulationStatus: [
                {
                    id: this.$_MEnum.EmulationStatus.Active, name: this.$_MResource['VN'].EmulationStatus.Active,
                },
                {
                    id: this.$_MEnum.EmulationStatus.NoneActive, name: this.$_MResource['VN'].EmulationStatus.NoneActive,
                }
            ],
            errorForm: [],
            isSaveAndCreate: true,
            formElements: []

        }

    },

    methods: {
        tabFormElements(event) {
            event.preventDefault();
            if (this.errorForm.length == 0) {
                tabElement(this.formElements)
            } else {
                if (typeof this.errorForm[0].focus === 'function') {
                    this.errorForm[0].focus();
                }
            }
        },
        /**
         * @description thực hiện thêm các input element từ component con vào formElements 
         * @param {Object} elem 1 thành phần được nhận được từ component con
         */
        updateElement(elem) {
            this.formElements.push(elem)
        },
        /** 
         * @description  lấy danh sách thi đua từ api 
         */
        getAwards() {
            try {
                this.serviceAward
                    .getAll()
                    .then((res) => {
                        this.rewardlevers = res.data
                    })

            } catch (e) {
                console.log("lỗi lấy api get ở competitoinList ~" + e)
            }
        },
        /**
         * tạo dữ liệu ban đầu cho form
         * @param { } formName tên form 
         */
        createEmulation(formName) {
            try {
                let emulationInfo = {}
                if (formName == this.$_MResource["VN"].FormName.AddEmulationForm) {
                    emulationInfo.EmulationCode = "D13"
                    emulationInfo.AwardID = "142cb08f-7c31-21fa-8e90-67245e8b283e",
                        emulationInfo.EmulationAwardRecipient = this.$_MEnum.EmulationAwardRecipient.Individual,
                        emulationInfo.EmulationMovementType = this.$_MEnum.EmulationMovementType.Frequent,
                        emulationInfo.EmulationStatus = this.$_MEnum.EmulationStatus.Active,
                        emulationInfo.EmulationName = "",
                        emulationInfo.EmulationNote = ""
                }
                // lấy emulation từ component cha gửi đến
                else {
                    emulationInfo.EmulationCode = this.emulationTitle.EmulationCode
                    emulationInfo.AwardID = this.emulationTitle.AwardID
                    emulationInfo.EmulationAwardRecipient = this.emulationTitle.EmulationAwardRecipient
                    emulationInfo.EmulationMovementType = this.emulationTitle.EmulationMovementType
                    emulationInfo.EmulationStatus = this.emulationTitle.EmulationStatus
                    emulationInfo.EmulationName = this.emulationTitle.EmulationName
                    emulationInfo.EmulationNote = this.emulationTitle.EmulationNote
                    emulationInfo.EmulationID = this.emulationTitle.EmulationID
                }
                return emulationInfo;
            } catch (e) {
                console.log("lỗi khi gán dữ liệu ở form ~ " + e.message)
            }
        },
        /**
         * @description : đóng form khi click vào nút close
         * @param(any) 
         * @author :ntsy (29/06/2023)
         */
        closeForm() {
            this.$emit('closeForm');
        },
        /**
         * @description : thực hiện thêm ô input lỗi vào errorForm,
         * @param {*} errorData object do component textInput gửi cho chứa ô input lỗi(errorData.name) 
         * và thông báo lỗi(errorData.mess) 
         * @author : ntsy (29/06/2023)
         */
        onValidateInput(errorData) {
            if (!this.errorForm.includes(errorData.name)) {
                if (this.$refs.form1.children[0].children[1].children[0] == errorData.name) this.errorForm[0] = errorData.name
                else if (this.$refs.form2.children[0].children[1].children[0] == errorData.name) this.errorForm[1] = errorData.name
                else this.errorForm.push(errorData.name)
            }
        },
        /**
         * @description : thực hiện lưu dữ liệu khi không tồn tại validate, nếu có lỗi, focus và ô nhập liệu đầu tiên
         * @param(any) 
         */
        saveEmulation(isSaveAndCreate) {
            try {
                this.errorForm = this.errorForm.filter(elem => elem != undefined)
                // nếu có lỗi và ô bị lỗi có thể focus được thì sẽ focus vào
                if (this.errorForm.length > 0) {
                    if (typeof this.errorForm[0].focus === 'function') {
                        this.errorForm[0].focus();
                    }
                } else {
                    if (this.formName == this.$_MResource["VN"].FormName.AddEmulationForm) {
                        this.serviceEmulationTitle.post(this.emulation)
                            .then(res => {
                                this.closeForm()
                                this.$emit("UpdateFormSuccess", isSaveAndCreate)
                            })
                            .catch(err => {
                                if (err.response.data.ErrorCode == 409) {
                                    this.$emit("UpdateFormFalse", this.emulation.EmulationCode)
                                } else this.$emit("handlerException", err.response.data.UserMess)
                            })
                    } else {
                        this.serviceEmulationTitle.put(this.emulation)
                            .then(res => {
                                this.$emit("UpdateFormSuccess", isSaveAndCreate)
                                this.closeForm()
                            })
                            .catch(err => {
                                if (err.response.data.ErrorCode == 409) {
                                    this.$emit("UpdateFormFalse", this.emulation.EmulationCode)
                                } else this.$emit("handlerException", err.response.data.UserMess)
                            });
                    }

                }
            } catch (err) {
                console.error(err)
            }
        },
        /**
         * @description :cập nhập giá trị cho form khi người dùng nhập input và xoá thành phần không có lỗi trong errorForm
         * @param {*} dataInput 1 object gồm ô input(dataInput.name) và giá trị input(dataInput.newValue)
         * @param {*} instanceUpdate trường được cập nhập giá trị mới 
         */
        updateValue(dataInput, instanceUpdate) {
            // gán giá trị cho emulation 
            this.emulation[instanceUpdate] = dataInput.newValue;
            // sau khi có giá trị hợp lệ xoá => xoá ô input bị lỗi
            this.errorForm = this.errorForm.filter(elem => {
                return (elem != dataInput.name)
            })

        },
        /**
         * @description: thực hiện focus vào ô input đầu tiên của form
         * @param(any)
         */
        onfocusFirstInput() {
            this.$refs.form1.children[0].children[1].children[0].focus()
        },
        /**
         * thực hiện gọi hàm lưu khi ấn ctrl + s
         * @param {*} event đối tượng event của sự kiện keydow
         */
        saveByKeyboard(event) { 
            if (event.ctrlKey && event.key == 's') {
                event.preventDefault();
                this.saveEmulation(!this.isSaveAndCreate)
            } else if(event.ctrlKey && event.shiftKey && event.key == 'S') {
                event.preventDefault();
                this.saveEmulation(this.isSaveAndCreate)
            }
        },

    },
    props: {
        /** tên form, hiện thị đúng form mong muốn khi thao tác ở component cha gồm "Thêm danh hiệu thi đua" và "Sửa danh hiệu thi đua*/
        formName: {
            type: String,
            default: MResource['VN'].FormName.AddEmulationForm
        },
        // thông tin danh hiệu thi đua nhận được khi click vào nút sửa
        emulationTitle: {
            type: Object
        },
        // các trường trong form có thể disable không
        disabled: {
            type: Boolean,
            default: false
        }
    }
}
</script>

<style>
@import url('../../styles/base/form.css');
</style>