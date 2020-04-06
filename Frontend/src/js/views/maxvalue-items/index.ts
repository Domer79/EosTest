import Vue from "vue";
import Component from "vue-class-component";
import ParentItemsComponent from "src/js/components/parent-items";
import { ItemGroup } from "vuetify";
import Item from "src/js/models/item";
import { Watch, Prop } from "vue-property-decorator";
import ChildItemsComponent from "src/js/components/child-items";

@Component({
    template: require('./template.html'),
    components: {
        "maxvalue-items": ParentItemsComponent,
        "child-items": ChildItemsComponent
    }
})
export default class MaxValueItemsView extends Vue{
    @Prop({ default: false }) readonly cte: boolean;
    parentItem: Item = null;
}