<template>
    <div class="input-wrap" :class="{'is-valid': hasError && isValidate}" @keyup.enter="isSearchInput? searchInput():null" > 
        <label v-if="isLabel" for="" :class="[{request:isRequest},'form-label']" >{{ labelText }}</label>
        <div :class="[nameInput, 'input-box', {'input-disabled':disabled}]" ref="inputElement">
            <input type="text" class="input" :placeholder="textPlaceholder" 
            v-model="inputValue" @blur="isSearchInput? searchInput() : checkInput(inputValue)" ref="input" @focus="handlerFocus"
            :class="{'input-disabled':disabled}"> 
            <div v-if="isIconClose && isFocus" class="input__icon" @click="clearInput" >
                <div class="icon icon-close"></div>
            </div>
            <div v-if="isIcon" class="input__icon" @click="searchInput" >
                <div class="icon" :class="nameIcon"></div>
            </div>
            
         </div> 
         <span v-if="hasError && isValidate" class="text-error-mess">{{ errorMess }}</span>
    </div>
</template>
<script> 
export default {
    name:'text-input',
    mounted(){
        // thực hiện gửi đi phần tử input cho component cha xử lý
        this.$emit("sendInputElement",this.$refs.input)
    },
    data() {
        return {
            inputValue:this.defaultValue,
            hasError:false,
            errorMess:"",
            isIconClose:false,
            isFocus:false
        }
    },
    methods:{
        /**
         * @description thực hiện chọn tất cả giá trị của input khi đc focus vào lần đâu tiên
         */
        handlerFocus(){
            this.isFocus = true
            if(this.inputValue.trim() != "" ) {
                this.$refs.input.select()
            }
        },
        /**
         * @description thực hiện clear input khi click vào icon close
         */
        clearInput(){
            this.inputValue=""
            console.log(this.inputValue)
        },
         /**
         * @description :-nếu giá trị nhập vào rỗng thì gửi cho component cha 1 object chứa ô input lỗi, thông báo lỗi và hiện thị lỗi ra màn hình
         *               -nếu có giá trị thì cập nhập giá trị trong component cha và gửi cho component cha 1 object chứa ô input và giá trị input
         * @param {*} value giá trị ô input khi người dùng nhập vào
         */
      checkInput(value) {
        try{
                if(value.trim()=="") {
                    this.isIconClose = false
                    this.hasError = true;
                    this.errorMess =  `${this.labelText} ${this.$_MResource['VN'].InvalidError.Empty}` 
                   let errorObject ={
                        name:this.$refs.inputElement.children[0], // thành phần input
                        mess: this.errorMess
                    }
                    this.$emit("onValidateInput",errorObject)
                }else {
                    this.isIconClose=true
                    this.hasError = false;
                    let valueObject = {
                        name:this.$refs.inputElement.children[0], // thành phần input
                        newValue : value
                    }
                    this.$emit('updateValue', valueObject)
                }

            } catch(e) {
                console.log("error in TextInput.vue ~ " + e)
            }
      },
      // thực hiện gọi hàm xử lí ở component cha và gửi giá trị ở ô input
      searchInput(){
        this.checkInput(this.inputValue)
        this.isFocus = false
        this.$refs.input.blur()
        this.$emit("searchInput",this.inputValue)
      }
    },
    props:{
        /**placeholder của input */
        textPlaceholder : {
            type: String,
            default: "nhập dữ liệu"

        },

        /**bên cạch khung tìm khiếm có icon hay không */
        isIcon:{
            type :Boolean,
            default: false
        },
        /** nếu có icon thì nhập tên của icon */
        nameIcon:{
            type: String,
            default:"icon-search"
        },

        /**trên ô tìm kiếm có label không */
        isLabel:{
            type:Boolean,
            default: false
        },
        /** nội dung của label */
        labelText:{
            type:String,
            default:""
        },
        /** tên của input có thể là select-input khi đó input có width là 100%  */
        nameInput:{
            type:String,
            default:"text-input-wrap"
        },

        /** trường dữ liệu có bắt buộc phải nhập không */
        isRequest:{
            type:Boolean,
            defaule:false
        },
        /** dữ liệu dữ liệu ban đầu*/
        defaultValue:{
            required:true
        },
        /** trường tìm kiếm bình thường thì sẽ không có cần validate */
        isValidate:{
            type:Boolean,
            default:true
        },
        // có phải là thanh tìm kiếm hay không
        isSearchInput:{
            type:Boolean,
            default:false
        },
        maxLength:{
            type: Number,
            default:254
        },
        // input có phải disable không
        disabled:{
            type:Boolean,
            default:false
        }
    },
    watch:{
        /**kiểm tra mỗi khi giá trị input thay đổi  */
        inputValue(newValue) {
            if(newValue.length > this.maxLength ) {
                this.inputValue = newValue.slice(0, this.maxLength);
            }
            this.checkInput(newValue);
        },

    },
}
</script>

<style>
@import url('../styles/base/input.css');
</style>