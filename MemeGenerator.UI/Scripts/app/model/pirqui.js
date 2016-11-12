var pirqui = new Vue({
    el: "#pirqui",
    data: {
        message: ""
    },
    computed: {
        convertMessage: function () {
            return this.message.split("a").join("i")
                               .split("e").join("i")
                               .split("i").join("i")
                               .split("o").join("i")
                               .split("u").join("i");
        }
    }
});