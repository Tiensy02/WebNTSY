import Vue from 'vue';
import App from './App.vue';
import TextInput from './components/TextInput.vue';
import MButton from './components/MButton.vue';
import MCombobox from './components/MCombobox.vue';
import MCheckbox from './components/MCheckbox.vue';
import MDropdownList from './components/MDropdownList.vue';
import MDialog from './components/MDialog.vue';
import SelectedInput from './components/SelectedInput.vue';
import MToastMess from './components/MToastMess.vue';
import MResource from './helper/resourse';
import EmulationTitleForm from './views/EmulationTitle/EmulationTitleForm.vue';
import EmulationTitleFilter from './views/EmulationTitle/EmulationTitleFilter.vue';
import EmulationTitles from './views/EmulationTitle/EmulationTitles.vue';
import MContextMenu from './components/MContextMenu.vue';
import MTextArea from './components/MTextArea.vue'
import MRadioInput from './components/MRadioInput.vue';
import MTable from './components/MTable.vue';
import MEnum from './enum/enum.js'
import store from '../src/store/store.js';

Vue.component("m-radio-input",MRadioInput)
Vue.component("m-context-menu",MContextMenu);
Vue.component("text-input", TextInput);
Vue.component("m-button", MButton);
Vue.component("m-toast-mess", MToastMess);
Vue.component("m-combobox", MCombobox);
Vue.component("m-dialog", MDialog);
Vue.component("m-text-area",MTextArea);
Vue.component("selected-input", SelectedInput);
Vue.component("m-table", MTable);
Vue.component("m-dropdown-list", MDropdownList);
Vue.component("emulation-title-form", EmulationTitleForm);
Vue.component("emulation-title-filter", EmulationTitleFilter);
Vue.component("emulation-titles", EmulationTitles);
Vue.component("m-checkbox", MCheckbox);


Vue.prototype.$_MResource = MResource;
Vue.prototype.$_MEnum=MEnum;

new Vue({
  store,
  render: h => h(App)
}).$mount('#app');
