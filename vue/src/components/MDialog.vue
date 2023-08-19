<template>
    <div class="modal-overlay" @keydown.tab="toggleButtonFocus">
        <div class="dialog-wrap">
            <div class="dialog-main">
                <div class="dialog-header flex">
                    <div class="dialog-title">{{ dialogTitle }}</div>
                    <div class="icon icon-close" @click="closeDialog"></div>
                </div>
                <div class="dialog-content" v-html="dialogMess"></div>
            </div>
            <div class="dialog-bottom" ref="dialogBottom">
                <div v-if="hasManyButton">
                        <m-button :textButton="textButtonSecondary" @handlerClickButton="closeDialog"></m-button>
                        <m-button btnClass="btn-close" :textButton="textButtonPrimary" @handlerClickButton="handlerClickButtonPrimary"></m-button>
                </div>
                <div v-else>
                    <m-button :btnClass="handlerDeletel ? '' : 'btn-close'" textButton="Đóng" @handlerClickButton="closeDialog">
                    </m-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "m-dialog",
    mounted() {
        // gán dữ liệu cho mảng chứa các butotn
        this.buttons = this.$refs.dialogBottom.children[0].children
        this.indexButtonFocus = this.buttons.length
    },
    watch: {
        buttons(newButtons) {
            newButtons[newButtons.length - 1].focus()
        },
    },
    data() {
        return {
            buttons: null,
            indexButtonFocus: null,
        }
    },

    props: {
        // tiêu đề của dialog
        dialogTitle: {
            type: String,
            default: "MISA CeGov"
        },
        // nôi dung thông báo
        dialogMess: {
            type: String,
            default: ""
        },
        // tên button phụ
        textButtonSecondary: {
            type: String
        },
        // tên của button chính
        textButtonPrimary: {
            type: String
        },
        // diaglof có nhiều button hay không
        hasManyButton: {
            type: Boolean,
            default: false
        },
        // nếu là hành động xóa thì hiện nút đóng bth
        handlerDeletel: {
            type: Boolean,
            default: false
        }

    },
    methods: {
        closeDialog() {
            this.$emit('closeDialog')
        },
        /**
         * thực hiện chuyển đổi focus giữa các button trong dialog
         * @param {*} event data của event data
         */
        toggleButtonFocus(event) {
            event.preventDefault();
            this.indexButtonFocus++;
            if (this.indexButtonFocus >= this.buttons.length) {
                this.indexButtonFocus = 0
            } else {
                this.indexButtonFocus = this.indexButtonFocus
            }
            this.buttons[this.indexButtonFocus].focus();
        },
        handlerClickButtonPrimary(){
            this.$emit("handlerClickButtonPrimary")
        }
        
    }

}
</script>

<style>
@import url(../styles/base/dialog.css);
</style>