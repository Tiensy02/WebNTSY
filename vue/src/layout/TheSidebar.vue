<template>
    <div class="sidebar">
        <div class="sidebar__menu">
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item1"></div>
                    <div class="sidebar__items_text">Tổng quan</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item2"></div>
                    <div class="sidebar__items_text">Thi đua</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item3"></div>
                    <div class="sidebar__items_text">Khen thưởng</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item4"></div>
                    <div class="sidebar__items_text" >Quyết định</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item5"></div>
                    <div class="sidebar__items_text">Hồ sơ</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item6"></div>
                    <div class="sidebar__items_text">Tra cứu</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item7"></div>
                    <div class="sidebar__items_text">Báo cáo</div>
                </div>
                <div class="menu-line"></div>
                <div class="sidebar__items item--active">
                    <div class="icon sidebar__items_head item8"></div>
                    <div class="sidebar__items_text">Danh mục</div>
                </div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item9"></div>
                    <div class="sidebar__items_text">Thiết lập</div>
                </div>
                <div class="menu-line"></div>
                <div class="sidebar__items">
                    <div class="icon sidebar__items_head item10"></div>
                    <div class="sidebar__items_text">Hướng dẫn</div>
                </div>
                
        </div>  
        <div class="sidebar__bottom">
            <div class="sidebar__bottom-wrap">
                <div class="icon icon-back"></div>
                <div class="sidebar__bottom__title">Thu gọn</div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name:"TheSidebar",
    data(){
        return{
            
        }
    }
}
</script>

<style>

@import url('../styles/layout/sidebar.css');
</style>