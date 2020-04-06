import axios from "src/js/axios";
import Vue from "vue";
import Component from "vue-class-component";

@Component({
    template: require("./template.html")
})
export default class InitialFillComponent extends Vue{
    loading: boolean = true;
    error: boolean = false;

    async mounted(){
        try {
            this.loading = true;
            await axios.post("item/initial-fill");
        } catch {
            this.error = true;
        } finally{
            this.loading = false;
        }
    }
}