<template>
    <div class="selected-input-wrap" >
        <label class="request form-label">{{ labelText }}</label>
        <div class="form-select flex">
            <div v-for="(item, index) in listAbel" :key="index" @click="disabled? null : onClickCheck(index,item)"
             class="checkbox-wrapper flex" :class="{'select-input-disabled':disabled}" 
             @mouseover="!disabled? hoverCheckbox(index) : null"
             @mouseout="!disabled? unHoverCheckbox(index) : null">
                <label class="checkbox-wrap" ref="checkboxWrap" :class="{'checkbox-disables':disabled}">
                    <input ref="checkInput" type="checkbox" :disabled="disabled"
                      class="check-box" @click="disabled? null: onClickCheck(index,item)"
                      @focus="hoverCheckbox(index)"  @blur="unHoverCheckbox(index)"
                      >
                    <span class="checkmak"></span>
                </label>
                <span class="check-name" ref="checkLabel" >{{ item[itemName] }}</span>
            </div>
        </div>
        <span v-if="hasError" class="text-error-mess">{{ errorMess }}</span>
    </div>
</template>

<script>
export default {
    name: "radio-input",
    created(){
         // thêm mới thuộc tính checked cho danh dách lựa chọn
         this.listAbel.forEach(item => {
            item.checked = false;
        }) 
    },
    mounted() {
        // gửi các input element cho component cha xử lí
        this.$refs.checkInput.forEach(ele => {
            this.$emit("sendInputElement",ele)
        })
        // thực hiện model dữ liệu khởi tạo
        this.selectDefaultValue(this.defaultValue);
        this.modealValueWithListAbelChecked();
        // cập nhập giá trị ban đầu
        this.updateElementSelect();
        // gọi hàm validate giá trị ban đầu
        this.onValidate(this.elementSelect)
        
    },
    data() {
        return {
            hasError: false,
            elementSelect: [], // mảng chứa những ô được tick
            errorMess: "",
        }
    },
    watch: {
        elementSelect(newValue) {
            this.onValidate(newValue)
        },
    },
    methods: {
        /**
         * @description hàm thực hiện thêm class hover vào cho checkbox-wrap khi di chuột vào phần tử chứa ô check
         * box và label của nó
         * @param {Number} index chỉ số của check wrap 
         */
        hoverCheckbox(index){
            this.$refs.checkboxWrap[index].classList.add("checkbox-wrap-hover")
        },
         /**
         * @description hàm thực hiện xóa class hover vào cho checkbox-wrap khi di chuột vào phần tử chứa ô check
         * box và label của nó
         * @param {Number} index chỉ số của check wrap 
         */
        unHoverCheckbox(index){
            this.$refs.checkboxWrap[index].classList.remove("checkbox-wrap-hover")
        },
        /**
         * @description:  click vào label của checkbox thì thực hiện checked và gán những checkbox 
         * bằng true vào elementSelect
         * @param {*} index chỉ số của checkbox 
         */
        onClickCheck( index,item) {
            //this.$refs.checkInput là 1 mảng chứa các checkbox
            this.$refs.checkInput[index].checked = !this.$refs.checkInput[index].checked
            if(this.$refs.checkInput[index].checked) {
                item.checked = true;
            }else item.checked = false
            this.updateElementSelect()
        },
        /**
         * thực hiện gán giá trị ban đầu
         * @param {*} listAbelID 
         */    
        selectDefaultValue(listAbelID) {
            let itemSelect = this.listAbel.find(item => item[this.itemID] == listAbelID)
            if(itemSelect != null) {
                itemSelect.checked = true
            } else{
                for(let i = 0 ; i < this.listAbel.length; i ++ ) {
                    this.listAbel[i].checked = true
                }
            }
        },
        /**
         * thực hiện model value checkbox với dữ liệu ban đầu
         */
        modealValueWithListAbelChecked(){
            for ( let i = 0 ; i < this.listAbel.length; i ++ ) {
                if(this.listAbel[i].checked) this.$refs.checkInput[i].checked = true;
                else this.$refs.checkInput[i].checked = false;
            }
        },
        /**
         * @description lọc ra những checkbox đã được tick
         */

        updateElementSelect(){
            this.elementSelect = this.listAbel.filter(ele => {
                return ele.checked;
            })
        },
        /**
         * validate 
         * @param {*} arraycheck 
         */
        onValidate(arraycheck) {
            /** nếu danh sách được tick = 0 thì sẽ khởi tạo errorMess và thông báo lỗi gọi hàm xử lí 
             * validate ở comp cha  đồng thời truyền cho comp cha đối tượng lỗi*/
            if (arraycheck.length == 0) {
                this.hasError = true
                this.errorMess = `${this.labelText} ${this.$_MResource['VN'].InvalidError.Empty} `
                let errorObject = {
                    name: this.labelText,
                    mess: this.errorMess
                }
                this.$emit("validateSelectInput",errorObject)
            } else  { // nếu danh sách được chọn tôn tại phần tử thì tạo đối tượng giá trị upate và gửi cho comp cha
                this.hasError = false;
                let valueObject = {
                    name: this.labelText,
                    newValue: 1,
                }
                if(arraycheck.length == 1) {
                    valueObject.newValue = arraycheck[0][this.itemID]
                } else {
                    valueObject.newValue = 0
                }
                this.$emit("updateSelctedInput",valueObject)
                
            }
        },
    },
    props: {
        // danh sách lựa chọn, chứa các đối tượng bắt buộc phải có ít nhất 2 trường: id và name
        listAbel: {
            type: Array,
            required: true
        },
        // tên của trường lưu giá trị id của listAbel
        itemID:{
            type: String,
            default: "id"
        },
        // tên của trường lưu giá trị name của listAbel
        itemName:{
            type: String,
            default: "name"
        },
        // tên nhãn
        labelText: {
            type: String,
            default: ""
        },
        // giá trị ban đầu là id của item được tick ban đầu
        defaultValue:{
            type:Number
        },
        // có bị disabled hay không
        disabled:{
            type:Boolean,
            default:false
        }

    },
}
</script>

<style>
@import url('../styles/base/checkbox.css');
</style>