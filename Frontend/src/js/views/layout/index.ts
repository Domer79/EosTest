import Vue from 'vue';
import Component from 'vue-class-component';
import { Prop } from 'vue-property-decorator';

@Component({
    template: require("./template.html")
})
export default class LayoutController extends Vue {

    constructor() {
        super();
    }

    drawer = null;
    

    mounted() {

    }
}
