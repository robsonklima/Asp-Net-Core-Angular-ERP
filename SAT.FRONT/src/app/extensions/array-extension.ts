export { };

export class GroupBy {
    public key: string;
    public value: any[] = [];
}

Array.prototype.groupBy = function (this: Array<any>, key: string): GroupBy[] {
    let newGroupByList: GroupBy[] = [];
    this.forEach(element => {
        if (newGroupByList.some(s => { return s.key == element[key] })) {
            newGroupByList.find(m => m.key == element[key]).value.push(element);
        }
        else {
            let group = new GroupBy();
            group.key = element[key];
            group.value.push(element);
            newGroupByList.push(group);
        }
    });
    return newGroupByList;
};


