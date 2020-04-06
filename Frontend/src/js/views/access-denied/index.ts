import Vue from "vue";
import Component from "vue-class-component";
import router from "src/js/router";

@Component({
  template: require("./template.html")
})
export default class AccessDeniedComponent extends Vue {
    goBack(){
        router.back();
    }
}