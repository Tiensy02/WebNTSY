<template>
        <button ref="buttonElement" :class="['btn', btnClass,{'tooltip--top' : hasTooltip} ]" 
            @click="(isContextMenu) ? toggleContexMenu() : handlerClickButton()"
            >
            <div v-if="isIcon" :class="['icon',iconName]"></div>
            <span>{{ textButton }}</span>
            <span class="tooltiptext">{{toolTipText }}</span>
            <m-context-menu v-if="isContextMenu && isShowContextMenu" @handlerClickMenu="handlerClickMenu" :items="menuItems"></m-context-menu>
        </button>
</template>

<script>
export default {
    name:'m-button',
    data() {
        return {
            isShowContextMenu:false
        }
    },
    mounted(){
        // thực hiện gửi button element cho component cha xử lí
        this.$emit("sendButtonElement",this.$refs.buttonElement)
    },
    methods:{
        // thực hiện chuyển đổi trạng thái ẩn hiện của menu
        toggleContexMenu(){
            this.isShowContextMenu= !this.isShowContextMenu

        },
        // sử lí khi có click vào button
        handlerClickButton(){
            this.$emit("handlerClickButton")
        },
        // xủ lí khi click vào item trong menu
        handlerClickMenu(id){
            this.$emit("handlerClickMenuInButton",id)
        }
    },
    props:{
        /**
         * tên các class của button muốn thêm vào có: }
         * "btn--primary": button chính
         * "btn--secondary": button phụ
         * "btn-icon": có icon trong button
         * "btn- close": btn đóng
         * "btn- delete" : btn xóa
         */
        btnClass:{
                type : String,
                default:""
        },

        // tên icon
        iconName:{
            type : String,
            default :""
        },
        // nọi dung của button
        textButton:{
            type: String,
        },
        // button có icon hay không
        isIcon:{
            type : Boolean,
            default :false
        },
        // button có context Menu không
        isContextMenu:{
            type:Boolean,
            default :false
        },
        // nếu có context Menu thì truyền menu vào
        menuItems:{
            type:Array
        },
        // button có tooltip hay không
        hasTooltip:{
            type:Boolean,
            default:false
        },
        // tooltip là gì
        toolTipText:{
            type:String,
            default:""
        }
    }
}
</script>

<style>
@import url('../styles/base/button.css');
</style>