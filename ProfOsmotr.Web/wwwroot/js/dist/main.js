(()=>{"use strict";var e={789:(e,t,a)=>{a(755),a(734),a(688),a(686),a(112),a(21),a(41),a(6),a(852),a(168);async function n(e){if(400===e.status){let t=await e.json();if(!1===t.success){let e="Во время обработки запроса возникла ошибка. Обратитесь к администратору.\n";for(let a of t.errors)e+=a+"\n";alert(e)}}else alert("Во время обработки запроса произошла ошибка. Код ошибки: "+e.status)}const i=function(e){return parseFloat(e.replace(",",".").replace(/\s/g,""))},s=function(e){return e.toLocaleString(void 0,{minimumFractionDigits:2,maximumFractionDigits:2,useGrouping:!1})},o=function(e){return"string"!=typeof e?null:e.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#039;")},r=function(e){for(let t in e)return!1;return!0},l=function(e){let t=document.createElement("div");return t.innerHTML=e,t.firstElementChild},d=async function(e){var t=await fetch(e,{credentials:"same-origin"});if(t.ok)return await t.json();await n(t)},c=async function(e,t){var a=await fetch(e,{method:"POST",headers:{"Content-Type":"application/json;charset=utf-8"},credentials:"same-origin",body:JSON.stringify(t)});if(a.ok)return await a.json();await n(a)};function u(e,t){this.invalidityMessage,this.inputNode=e,this.validityCheck=t,this.registerListener()}function m(e,t){this.invalidityMessage=e,this.isInvalid=t}function p(e){for(var t=0;t<e.length;t++)e[t].CustomValidation&&e[t].CustomValidation.checkInput()}function h(e,t){e.CustomValidation=new u(e,t)}function f(e){for(let t of e)t.classList.remove("is-valid"),t.classList.remove("is-invalid")}u.prototype={checkValidity:function(e){let t=this.validityCheck.isInvalid(e);this.invalidityMessage=t?this.validityCheck.invalidityMessage:null,this.rerenderValidity(e,t)},checkInput:function(){if(this.inputNode.CustomValidation.invalidityMessage=null,this.checkValidity(this.inputNode),null==this.inputNode.CustomValidation.invalidityMessage)this.inputNode.setCustomValidity("");else{var e=this.inputNode.CustomValidation.invalidityMessage;this.inputNode.setCustomValidity(e)}},registerListener:function(){var e=this;this.inputNode.addEventListener("keyup",(function(){e.checkInput()}))},rerenderValidity:function(e,t){const a="is-valid",n="is-invalid",i="invalid-feedback";let s=e.nextSibling;if(t){if(e.classList.add(n),e.classList.remove(a),!s||!s.classList||!s.classList.contains(i)){let t=document.createElement("div");t.classList.add(i),t.innerText=this.invalidityMessage,e.parentNode.insertBefore(t,s)}}else e.classList.add(a),e.classList.remove(n),s&&s.classList&&s.classList.contains(i)&&s.remove()}};const v={phone:new m("Должен соответствовать шаблону: +7 123 4567890",(e=>!e.value.match(/^\+7 \d{3} \d{7}$/))),email:new m("Должно быть похоже на xxx@yyyy.zz",(e=>!e.value.match(/\S+@\S+\.\S+/)||e.value.length>300)),username:new m("Может содержать только буквы латинского алфавита и цифры. От 3 до 20 символов",(e=>!e.value.match(/^[0-9A-Za-z]{3,20}$/))),password:new m("Может содержать только буквы латинского алфавита, цифры и спецсимволы. От 8 до 20 символов",(e=>!e.value.match(/^[A-Za-z\d!@#$%^&*_+-?]{8,20}$/))),requiredText500:new m("Обязательное. До 500 символов",(e=>!e.value.match(/^.{1,500}$/))),requiredText70:new m("Обязательное. До 70 символов",(e=>!e.value.match(/^.{1,70}$/))),requiredText20:new m("Обязательное. До 20 символов",(e=>!e.value.match(/^.{1,20}$/))),positiveInteger:new m("Число должно быть целым и неотрицательным",(e=>""===e.value||!Number.isInteger(+e.value)||+e.value<0)),price:new m("Значение должно быть неотрицательным, целым либо с двумя знаками после точки",(e=>!e.value.match(/^\d+([,\.]\d\d)?$/)))};var y=a(755);async function b(){const e=await d("/api/order/getItemsList");e&&(g("#OrderItems1",e.annex1),g("#OrderItems2",e.annex2))}function g(e,t){y(e).select2({data:t.map(x),multiple:!0,placeholder:"Найти по номеру пункта...",theme:"bootstrap4",width:y(this).data("width")?y(this).data("width"):y(this).hasClass("w-100")?"100%":"style"})}function x(e){let t=null==e.name?"":e.name;return{id:e.id,text:o(e.key+" "+t)}}var w=a(755);var _=a(755);var I=a(755);const T="d-none",E="true",S=class{constructor(e){this._modalElement=null,this._form=null,this._dataElements=null,this.model=null,this.buttonsData=[],this.options=e,this._build()}enableButtons(){for(let e of this.buttonsData)e.element.disabled=!1,!0===e.visibility||r(this.model)||e.visibility(this.model)?e.element.classList.remove(T):e.element.classList.add(T)}getId(){if(!this._id){let e=0,t=null,a=null;do{t="custom-modal-"+ ++e,a=document.getElementById(t)}while(a);this._id=t}return this._id}hide(){I(`#${this.getId()}`).modal("hide")}show(){this.show(null)}show(e){this.model=I.extend(!0,{},e),this._setTitle(),e&&this._seedData(),this.enableButtons(),I(`#${this.getId()}`).modal("show")}_build(){this._createModalElement(),this._createDataInputs(),this._createButtons(),this._addModalHiddenEventListener()}_addModalHiddenEventListener(){let e=this;I(`#${this.getId()}`).on("hidden.bs.modal",(function(){e._resetForm(),f(e._dataElements),e.model=null}))}_createModalElement(){this._modalElement=l('<div class="modal" data-backdrop="static" tabindex="-1" role="dialog" aria-hidden="true">\n<div class="modal-dialog  modal-dialog-centered modal-lg" role="document">\n<div class="modal-content">\n<div class="modal-header">\n<h5 class="modal-title"></h5>\n<button type="button" class="close" data-dismiss="modal" aria-label="Close">\n<span aria-hidden="true">&times;</span>\n</button>\n</div>\n<div class="modal-body">\n</div>\n<div class="modal-footer">\n</div>\n</div>\n</div>\n</div>'),this._modalElement.id=this.getId(),document.body.appendChild(this._modalElement)}_createDataInputs(){if(this.options.data){this._form=document.createElement("form");for(let e of this.options.data){let t=this._createDataElement(e);this._form.appendChild(t)}this._addFormEventListeners(),this._modalElement.querySelector(".modal-body").appendChild(this._form)}}_addFormEventListeners(){let e=this;this._dataElements=this._form.querySelectorAll(".js-custom-modal");for(let e of this._dataElements)e.addEventListener("keyup",t),e.addEventListener("change",t);function t(t){let a=t.target,n=a.dataset.customModalId,i=e._getPathById(n);i&&a.dataset.customModalRendered!==E&&e._updateModel(i,a.value)}}_createButtons(){if(!this.options.buttons)return;let e=this._modalElement.querySelector(".modal-footer");for(let t of this.options.buttons){let a=this._getButton(t);e.appendChild(a),this.buttonsData.push({element:a,visibility:"function"!=typeof t.visibility||t.visibility})}}_getButton(e){let t=document.createElement("button");t.className=e.className?e.className:"btn btn-primary",t.innerText=e.text;let a=this;return t.addEventListener("click",(async function(t){t.preventDefault(),t.target.disabled=!0,function(){let e=new Event("change");a._dataElements.forEach(((t,a,n)=>t.dispatchEvent(e)))}(),p(a._dataElements),a._form.checkValidity()?await e.action(a.model):t.target.disabled=!1})),t}_createDataElement(e){const t=e.readonly||this.options.readonly;let a=null;switch(e.type){case"input-text":a=i("text");break;case"input-password":a=i("password");break;case"textarea":a=s("textarea");break;case"select":return n(function(){let t=s("select");for(let a of e.options)t.options.add(a);return t}())}return e.validityCheck&&!e.readonly&&this._addValidation(e.validityCheck,a),e.render&&(a.dataset.customModalRendered=E),n(a);function n(t){let a=function(){let t=`<label style="display:block;"><span style="display:block; margin-bottom: .5rem;">${e.label}</span></label>`;return l(t)}();a.appendChild(t);let n=l('<div class="form-group"></div>');return n.appendChild(a),n}function i(e){let t=s("input");return t.type=e,t}function s(a){let n=document.createElement(a);return n.className="js-custom-modal form-control",n.dataset.customModalId=e.id,n.disabled=t,n}}_addValidation(e,t){"select"!==t.tagName&&h(t,e)}_getPathById(e){for(let t of this.options.data)if(t.id===e)return t.path;return null}_updateModel(e,t){let a=e.split("."),n=this.model;for(let e=0;e<a.length;e++)e===a.length-1?n[a[e]]=t:(n[a[e]]||(n[a[e]]={}),n=n[a[e]])}_getValue(e){if(null===e)return null;let t=this.model,a=e.split(".");for(let e=0;e<a.length;e++){if(e===a.length-1)return t[a[e]];t=t[a[e]]}}_setTitle(){const e=this.options.title,t=this._modalElement.querySelector(".modal-title");switch(typeof e){case"string":t.innerText=e;break;case"function":t.innerText=e(this.model)}}_seedData(){for(let e of this.options.data){let t=this._getValue(e.path);null!=t&&(this._modalElement.querySelector(`[data-custom-modal-id="${e.id}"]`).value=e.render?e.render(t):t)}}_resetForm(){this._form.reset()}};var q=a(755);const N={config:{select:{style:"single"},language:{decimal:"",emptyTable:"Нет данных для отображения",info:"Всего элементов: _TOTAL_",infoEmpty:"",infoFiltered:"(отфильтровано из _MAX_ элементов)",infoPostFix:"",thousands:",",lengthMenu:"_MENU_ элементов на страницу",loadingRecords:"Загрузка...",processing:"Загрузка...",search:"Поиск:",zeroRecords:"Ничего не найдено",paginate:{first:"Начало",last:"Конец",next:"Вперед",previous:"Назад"},aria:{sortAscending:": сортировка по возрастанию",sortDescending:": сортировка по убыванию"},select:{rows:{_:""}}},order:[[0,"asc"]],orderMulti:!1,processing:!0},scrollerConfig:{scrollY:450,scrollCollapse:!0,deferRender:!0,scroller:!0},serverSideAjax:{type:"POST",contentType:"application/json; charset=utf-8",data:e=>JSON.stringify(e)},dom:"<'row'<'col-sm-12 col-md-6'{BUTTON}><'col-sm-12 col-md-6'f>>\n\t\t<'row'<'col-sm-12'tr>>\n\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'{PAGINATION}>>"},C=class{constructor(e){this._options=e,this._applyOptions(),this._table=q("#"+this._options.tableId).DataTable(this._config)}getTable(){return this._table}ajaxReload(){this._config.ajax?this._table.ajax.reload():console.log("CustomDataTable: Невозможно перезагрузить данные для этой таблицы посредствам ajax")}_applyOptions(){this._config=q.extend(!0,{},N.config),this._options.ajaxUrl&&(this._config.ajax={url:this._options.ajaxUrl}),this._options.scroll&&(this._config={...this._config,...N.scrollerConfig}),this._options.serverSide&&(this._config.ajax?(this._config.serverSide=!0,Object.assign(this._config.ajax,N.serverSideAjax)):console.log("CustomDataTable: Для обработки таблиц на сервере, задайте ajaxUrl")),this._options.advanced&&Object.assign(this._config,this._options.advanced),this._applyDomConfig()}_applyDomConfig(){let e="",t="";this._config.buttons&&(e="B"),this._scroller||(t="p"),this._config.dom=N.dom.replace("{BUTTON}",e).replace("{PAGINATION}",t)}};var D=a(755);const O="SuccessToast",k=class{constructor(){this._createToastElement()}show(){D("#"+O).toast("show")}_createToastElement(){if(document.getElementById(O))return;let e=l('<div class="toast" id="SuccessToast" role="alert" data-delay="3000" \nstyle="position: fixed; bottom: 15px; right: 20px; min-width: 250px;">\n    <div class="toast-body alert-success font-weight-bold">\n        Операция выполнена успешно\n    </div>\n</div>');document.body.appendChild(e)}_initToast(){D("#"+O).toast()}};class M{constructor(){this.succesToast=new k}static init(){return(async()=>{const e=new M;await e._getExaminationsData(),e._createDataTable(),e._createExaminationModal()})()}async _getExaminationsData(){this._examinationsData=await d("/api/order/getExaminations")}_createDataTable(){const e={tableId:"Examinations",advanced:{data:this._getTableData(),ordering:!1,columns:[{data:"name",render:(e,t,a)=>o(e)},{data:"targetGroup.name",render:(e,t,a)=>o(e)},{data:"defaultServiceDetails.fullName",render:(e,t,a)=>o(e)}],buttons:[{text:"Добавить обследование",action:(e,t,a,n)=>{this._examinationModal.show(null)}},{extend:"selectedSingle",text:"Редактировать",action:(e,t,a,n)=>{let i=t.row({selected:!0}).data();i.editing=!0,this._examinationModal.show(i)}}]}};this.examinationsTable=new C(e).getTable()}_createExaminationModal(){const e={title:e=>e.editing?`Редактирование обследования: ${e.name}`:"Создание нового обследования",data:[{id:"target-group",path:"targetGroup.id",label:"Целевая группа",type:"select",options:this._examinationsData.targetGroups.map((e=>new Option(e.name,e.id)))},{id:"name",path:"name",label:"Название по приказу",type:"textarea",validityCheck:v.requiredText500},{id:"service-code",path:"defaultServiceDetails.code",label:"Код услуги по умолчанию",type:"input-text",validityCheck:v.requiredText20},{id:"service-full-name",path:"defaultServiceDetails.fullName",label:"Полное наименование услуги по умолчанию",type:"textarea",validityCheck:v.requiredText500}],buttons:[{text:"Сохранить",action:async e=>await this._onSaveExamination(e)}]};this._examinationModal=new S(e)}_getTableData(){return this._examinationsData.orderExaminations.map((e=>this._convertToTableData(e)))}_convertToTableData(e){return{id:e.id,name:e.name,targetGroup:{id:e.targetGroupId,name:this._examinationsData.targetGroups.find((t=>t.id==e.targetGroupId)).name},defaultServiceDetails:e.defaultServiceDetails}}async _onSaveExamination(e){const t=this;let a,n={name:e.name,defaultServiceCode:e.defaultServiceDetails.code,defaultServiceFullName:e.defaultServiceDetails.fullName,targetGroupId:+e.targetGroup.id};a=e.editing?await async function(){const a=await c("/api/order/updateExamination",Object.assign(n,{id:e.id}));if(a)return t.examinationsTable.row(((e,t,n)=>t.id===a.id)).data(t._convertToTableData(a)).draw(),a}():await async function(){const e=await c("/api/order/addExamination",n);if(e)return t.examinationsTable.row.add(t._convertToTableData(e)).draw(),e}(),a?(this._examinationModal.hide(),this.succesToast.show()):this._examinationModal.enableButtons()}}const L=M;var j=a(755);const R='select[data-custom-modal-id="examinations"';class B{constructor(){this.succesToast=new k}static init(){return(async()=>{let e=new B;return await e._getOrderData(),e._createDataTable(),e._createModal(),e})()}async _getOrderData(){this._orderItems=await d("/api/order/getOrder");let e=await d("/api/order/getExaminationsMin");this._orderExaminationsMap=new Map(e.map((e=>[e.id,e.name])))}_createDataTable(){const e={tableId:"OrderItems",advanced:{data:this._getDataTableData(),ordering:!1,columns:[{data:"key",render:(e,t,a)=>o(e)},{data:"name",render:(e,t,a)=>o(e)},{data:"examinations",render:(e,t,a)=>e.map((e=>o(e.name))).join("<br>")}],rowGroup:{dataSrc:"annex.name"},buttons:[{text:"Добавить пункт",action:(e,t,a,n)=>{this._showModal(null)}},{extend:"selectedSingle",text:"Редактировать",action:(e,t,a,n)=>{let i=t.row({selected:!0}).data();i.editing=!0,this._showModal(i)}},{extend:"selectedSingle",text:"Удалить",action:async(e,t,a,n)=>{let i=t.row({selected:!0}).data();await this._removeItem(i)}}]}};this.orderTable=new C(e).getTable()}_createModal(){let e={title:e=>e.editing?`Редактирование пункта ${e.key}`:"Создание нового пункта",data:[{id:"annexId",path:"annex.id",label:"Приложение",type:"select",options:this._orderItems.annexes.map((e=>new Option(`Приложение ${e.id}`,e.id)))},{id:"key",path:"key",label:"Пункт",type:"input-text",validityCheck:v.requiredText70},{id:"name",path:"name",label:"Название",type:"textarea",validityCheck:v.requiredText500},{id:"examinations",path:"examinations",label:"Обследования",type:"select",options:[]}],buttons:[{text:"Сохранить",action:async e=>await this._onSave(e)}]};this.orderItemModal=new S(e),this._initSelect2()}_getDataTableData(){return this._orderItems.annexes.map((e=>e.orderItems.map((e=>this._convertToTableData(e))))).flat()}_convertToTableData(e){return{id:e.id,annex:{id:e.annexId,name:`Приложение ${e.annexId}`},key:e.key,name:e.name,examinations:e.orderExaminations.map((e=>({id:e,name:this._orderExaminationsMap.get(e)})))}}_initSelect2(){const e=Array.from(this._orderExaminationsMap.entries()).map((e=>({id:e[0],text:e[1]})));j(R).select2({data:e,multiple:!0,placeholder:"Найти по названию",theme:"bootstrap4"})}async _onSave(e){const t=this,a=j(R).select2("data").map((e=>parseInt(e.id)));let n;n=!0===e.editing?await async function(){const n=await c("/api/order/updateItem",{id:e.id,name:e.name,examinations:a});if(n)return t.orderTable.row(((e,t,a)=>t.id===n.id)).data(t._convertToTableData(n)).draw(),n}():await async function(){const n=await c("/api/order/addItem",{annexId:parseInt(e.annex.id),key:e.key,name:e.name,examinations:a});if(n)return t.orderTable.row.add(t._convertToTableData(n)).draw(),n}(),n?(this.orderItemModal.hide(),this.succesToast.show()):this.orderItemModal.enableButtons()}async _removeItem(e){confirm(`Вы действительно хотите удалить пункт "${e.key}"?`)&&(await c("/api/order/deleteItem",e.id)).succeed&&(this.orderTable.row(((t,a,n)=>a.id===e.id)).remove().draw(),this.succesToast.show())}_showModal(e){const t=[document.querySelector('select[data-custom-modal-id="annexId"'),document.querySelector('input[data-custom-modal-id="key"')];e?t.forEach((e=>e.disabled=!0)):t.forEach((e=>e.disabled=!1)),this.orderItemModal.show(e),j(R).val(e?.examinations.map((e=>e.id))).trigger("change")}}const P=B,A=new class{constructor(e){this.namespace=e}load(){const e=this._getPageId();this._fire("common"),this._fire(e)}_getPageId(){return document.body.dataset.page}_fire(e){""!==e&&this.namespace[e]&&"function"==typeof this.namespace[e]&&this.namespace[e]()}}({"calculation-company":async function(){const e=document.professionConstructor,t=document.querySelectorAll('#Constructor input:not([type="submit"])'),a=document.querySelectorAll(".js-validate"),n="#OrderItems1",i="#OrderItems2",s="#CompanyName",r=new class{constructor(e){this._options=e,this._modelMap=new Map,this._listElement=document.querySelector(this._options.target),this._processButtonRemoveItem()}add(){let e=this._options.itemTemplate,t={};this._options.data.forEach(((a,n,i)=>{let s=a.source();t[a.path]=s;let o="{"+n+"}";e.includes(o)&&(a.render&&(s=a.render(s)),e=e.replace(o,s))}));let a=this._renderNewItem(e);this._addEventListeners(a),this._modelMap.set(a,t)}getData(){return Array.from(this._modelMap.values())}remove(e){this._modelMap.delete(e),e.remove()}_addEventListeners(e){e.addEventListener("mouseenter",(e=>e.target.appendChild(this._buttonRemoveItem))),e.addEventListener("mouseleave",(e=>e.target.removeChild(this._buttonRemoveItem)))}_processButtonRemoveItem(){this._buttonRemoveItem=l('<button class="btn btn-danger btn-sm" style="position: absolute; right: 0; top: 0; opacity: 0.8">Удалить</button>'),this._buttonRemoveItem.addEventListener("click",(e=>{const t=e.target;t.disabled=!0,this.remove(t.parentElement),t.disabled=!1}))}_renderNewItem(e){let t=l('<div class="row align-items-center mb-2 position-relative"></div>');return t.innerHTML=e,this._options.reverse?this._listElement.insertBefore(t,this._listElement.firstChild):this._listElement.appendChild(t),t}}({target:"#ProfessionsList",itemTemplate:'<div class="col-sm-8">{0}</div><div class="col-sm-4">{1} чел.</div>',reverse:!0,data:[{source:()=>e.ProfessionName.value,path:"name",render:e=>o(e)},{source:()=>parseInt(e.NumberOfPersons.value),path:"numberOfPersons"},{source:()=>parseInt(e.NumberOfWomen.value),path:"numberOfWomen"},{source:()=>parseInt(e.NumberOfWomenOver40.value),path:"numberOfWomenOver40"},{source:()=>parseInt(e.NumberOfPersonsOver40.value),path:"numberOfPersonsOver40"},{source:()=>{let e=w(n).select2("data"),t=w(i).select2("data");return e.concat(t).map((e=>parseInt(e.id)))},path:"orderItems"}]});await b(),h(document.querySelector(s),v.requiredText70),h(e.ProfessionName,v.requiredText70),function(){const t=new m("Должна быть больше нуля и не меньше численности входящих подгрупп",(t=>function(t){let a=+t;return!Number.isInteger(a)||a<1||a<+e.NumberOfPersonsOver40.value||a<+e.NumberOfWomen.value||a<+e.NumberOfWomenOver40.value}(t.value)));h(e.NumberOfPersons,t);const a=new m("Должна быть больше числа женщин старше 40 лет и меньше общей численности",(t=>function(t){let a=+t;return!Number.isInteger(a)||a<0||a>+e.NumberOfPersons.value||a<+e.NumberOfWomenOver40.value}(t.value)));h(e.NumberOfPersonsOver40,a);const n=new m("Должна быть больше числа женщин старше 40 лет и меньше общей численности",(t=>function(t){let a=+t;return!Number.isInteger(a)||a<0||a>+e.NumberOfPersons.value||a<+e.NumberOfWomenOver40.value}(t.value)));h(e.NumberOfWomen,n);const i=new m("Должна быть меньше всех остальных групп, но не меньше нуля",(t=>function(t){let a=+t;return!Number.isInteger(a)||a<0||a>+e.NumberOfPersons.value||a>+e.NumberOfPersonsOver40.value||a>+e.NumberOfWomen.value}(t.value)));h(e.NumberOfWomenOver40,i),function(){const e=Array.from(document.getElementsByClassName("js-number"));e.forEach((t=>t.addEventListener("keyup",(t=>p(e)))))}()}(),document.querySelector("#AddProfession").addEventListener("click",(function(a){a.preventDefault(),p(t),e.checkValidity()&&(r.add(),e.reset(),w(n).val(null).trigger("change"),w(i).val(null).trigger("change"),f(t))})),document.querySelector("#CreateCompanyCalculation").addEventListener("click",(async function(e){if(e.preventDefault(),p(a),!document.companyData.checkValidity())return;let t={name:document.querySelector(s).value,professions:r.getData()};if(0===t.professions.length)return void alert("Добавьте хотя бы одну профессию");let n=await c("/api/calculation/create",t);n&&(location="/Calculation/Result/"+n.id)}))},"calculation-edit":function(){const e=document.querySelectorAll(".js-result-price"),t=document.querySelectorAll(".js-result-amount"),a=document.querySelectorAll(".js-result-sum"),n=document.querySelectorAll(".js-result-group");function o(e){let t=e.target;if(m(t))return;let a=d(t),n=b(a);u(n)||(g(a).value=s(l(t,n)),w())}function r(e){let t=e.target;if(u(t))return;let a=d(t),n=y(a);m(n)||(g(a).value=s(l(n,t)),w())}function l(e,t){return i(e.value)*t.value}function d(e){return e.parentNode.parentNode.id}function u(e){return v.positiveInteger.isInvalid(e)}function m(e){return v.price.isInvalid(e)}function f(e){const t=e.target,a=d(t),n=y(a),i=b(a),s=g(a);!function(e,t){m(e)&&(e.value="0"),u(t)&&(t.value="0"),p([e,t])}(n,i);let o=[n,i,s];0===t.selectedIndex?function(e){e.forEach((e=>e.type="text"))}(o):function(e){e.forEach((e=>e.type="hidden"))}(o),w()}function y(e){return x(e,"price")}function b(e){return x(e,"amount")}function g(e){return x(e,"sum")}function x(e,t){return document.querySelector("#"+e+" .js-result-"+t)||(console.log("Не найдено поле "+t),null)}function w(){let e=0;for(var t=0;t<a.length;t++)"text"===a[t].type&&(e+=i(a[t].value));document.querySelector("#TotalSum").innerText=s(e)}Array.from(e).forEach((e=>{e.addEventListener("keyup",o),h(e,v.price)})),Array.from(t).forEach((e=>{e.addEventListener("keyup",r),h(e,v.positiveInteger)})),Array.from(n).forEach((e=>{e.addEventListener("change",f)})),w(),document.querySelector("#SaveChanges").addEventListener("click",(async function(a){if(a.preventDefault(),p(e),p(t),!document.Results.checkValidity())return;let n=function(){const e=parseInt(document.querySelector("#CalculationId").value);let t=[],a=document.querySelectorAll(".form-group");for(var n=0;n<a.length;n++){let e=(s=a[n],{id:parseInt(s.id.slice("r-".length)),price:i(s.querySelector(".js-result-price").value),amount:+s.querySelector(".js-result-amount").value,groupId:+s.querySelector(".js-result-group").value});t.push(e)}var s;return{calculationId:e,resultItems:t}}();const s=await c("/api/calculation/update",n);s&&(location="/Calculation/Result/"+s.id)}))},"calculation-single":async function(){const e=document.querySelector("#ProfessionName");await b(),h(e,v.requiredText70),document.querySelector("#createSingleCalculation").addEventListener("click",(async function(t){if(t.preventDefault(),p([e]),!document.singleCalc.checkValidity())return;const a=function(){let t=e.value,a=_("#OrderItems1").select2("data"),n=_("#OrderItems2").select2("data"),i=a.concat(n).map((e=>parseInt(e.id)));if(0==i.length)return void alert("Выберите хотя бы один пункт приказа");let s=document.querySelector("#IsWoman").checked,o=s?1:0,r=document.querySelector("#IsOver40").checked;return{name:"Индивидуальный расчет",professions:[{name:t,numberOfPersons:1,numberOfWomen:o,numberOfWomenOver40:s&&r?1:0,numberOfPersonsOver40:r?1:0,orderItems:i}]}}(),n=await c("/api/calculation/create",a);n&&(location="/Calculation/Result/"+n.id)}))},"catalog-list":function(){const e=".js-catalog-code",t=".js-catalog-name",a=".js-catalog-price",n=".js-catalog-group",o=new k,r={title:e=>`Изменение услуги для обследования: ${e.name}`,data:[{id:"service-code",path:"code",label:"Код услуги",type:"input-text",validityCheck:v.requiredText20},{id:"service-name",path:"fullName",label:"Наименование услуги",type:"textarea",validityCheck:v.requiredText500},{id:"service-price",path:"price",label:"Цена",type:"input-text",validityCheck:v.price},{id:"service-availability-group",path:"serviceAvailabilityGroupId",label:"Доступность",type:"select",options:[new Option("Доступна","1"),new Option("Недоступна","2"),new Option("Включена","3")]}],buttons:[{text:"Сохранить",action:async r=>await async function(r){const d={orderExaminationId:r.id,fullName:r.fullName,code:r.code,price:i(r.price),serviceAvailabilityGroupId:+r.serviceAvailabilityGroupId},u=await c("/api/catalog/update",d);u?(function(i){const o=document.querySelector(`tr[data-examination-id="${i.orderExaminationId}"]`);o.querySelector(e).innerText=i.code,o.querySelector(t).innerText=i.fullName,o.querySelector(a).innerText=s(i.price),o.querySelector(".js-catalog-update-time").innerText=new Date(i.updateTime).toLocaleString();const r=o.querySelector(n);r.innerText=i.serviceAvailabilityGroupName,r.dataset.groupId=i.serviceAvailabilityGroupId}(u),l.hide(),o.show()):l.enableButtons()}(r)}]},l=new S(r);new C({tableId:"Catalog",advanced:{buttons:[{extend:"selectedSingle",text:"Редактировать",action:function(i,s,o,r){let d={id:+(c=s.row({selected:!0}).node()).dataset.examinationId,name:c.querySelector(".js-catalog-examination").innerText,code:c.querySelector(e).innerText,fullName:c.querySelector(t).innerText,price:c.querySelector(a).innerText,serviceAvailabilityGroupId:c.querySelector(n).dataset.groupId};var c;l.show(d)}}]}})},"clinic-list":function(){const e="ClinicsList",t=new k,a=(document.getElementById(e).dataset.clinic,new S({title:"Информация о медицинской организации",readonly:!0,data:[{id:"full-name",path:"clinicDetails.fullName",type:"input-text",label:"Полное наименование"},{id:"short-name",path:"clinicDetails.shortName",type:"input-text",label:"Сокращенное наименование"},{id:"address",path:"clinicDetails.address",type:"input-text",label:"Адрес"},{id:"phone",path:"clinicDetails.phone",type:"input-text",label:"Телефон"},{id:"email",path:"clinicDetails.email",type:"input-text",label:"Электронная почта"},{id:"block-status",path:"isBlocked",type:"input-text",label:"Статус",render:e=>!0===e?"Заблокирована":"Активна"}],buttons:[{text:"Заблокировать",className:"btn btn-danger",action:async e=>await i(e,!0),visibility:e=>!e.isBlocked},{text:"Разблокировать",className:"btn btn-success",action:async e=>await i(e,!1),visibility:e=>e.isBlocked}]})),n=new C({tableId:e,ajaxUrl:"/api/clinic/list",serverSide:!0,scroll:!0,advanced:{buttons:[{extend:"selectedSingle",text:"Просмотр",action:(e,t,n,i)=>{let s=t.row({selected:!0}).data();a.show(s)}}],columns:[{data:"id",name:"Id",searchable:!1},{data:"clinicDetails.shortName",name:"ClinicDetails.ShortName",render:(e,t,a)=>o(e)},{data:"clinicDetails.phone",name:"ClinicDetails.Phone",render:(e,t,a)=>o(e),orderable:!1},{data:"clinicDetails.email",name:"ClinicDetails.Email",render:(e,t,a)=>o(e),orderable:!1},{data:"isBlocked",name:"IsBlocked",render:(e,t,a)=>e?"Заблокирована":"Активна",searchable:!1,orderable:!1}]}});async function i(e,i){const s={id:e.id,needBlock:i};await c("/api/clinic/manageClinic",s)?(a.hide(),n.ajaxReload(),t.show()):a.enableButtons()}},"clinic-settings":function(){const e=new k,t=document.getElementById("ClinicFullName"),a=document.getElementById("ClinicShortName"),n=document.getElementById("ClinicAddress"),i=document.getElementById("ClinicPhone"),s=document.getElementById("ClinicEmail");h(t,v.requiredText500),h(a,v.requiredText500),h(n,v.requiredText500),h(i,v.phone),h(s,v.email);const o=document.getElementById("SaveClinicDetails");o.addEventListener("click",(async function(t){t.preventDefault(),o.disabled=!0;const a=document.querySelectorAll("input");p(a);const n=document.ClinicDetails;if(!n.checkValidity())return void(o.disabled=!1);const i=new FormData(n),s=Object.fromEntries(i);await c("/api/clinic/updateDetails",s)&&(f(a),e.show()),o.disabled=!1}))},"order-examinations":L.init,"order-items":P.init,"register-create-request":function(){const e=document.getElementById("CandidateFullName"),t=document.getElementById("CandidateShortName"),a=document.getElementById("CandidateAddress"),n=document.getElementById("CandidatePhone"),i=document.getElementById("CandidateEmail"),s=document.getElementById("CandidateModeratorName"),o=document.getElementById("CandidateModeratorPosition"),r=document.getElementById("CandidateModeratorUsername"),l=document.getElementById("CandidateModeratorPassword"),d=document.querySelectorAll("input"),u=document.getElementById("SubmitRegisterRequest");u.addEventListener("click",(async function(m){if(m.preventDefault(),u.disabled=!0,p(d),!document.RegisterRequest.checkValidity())return void(u.disabled=!1);let h={fullName:e.value,shortName:t.value,address:a.value,phone:n.value,email:i.value,user:{name:s.value,position:o.value,username:r.value,password:l.value}};await c("/api/clinic/addRegisterRequest",h)&&(document.RegisterRequest.classList.add("d-none"),document.querySelector(".alert-success").classList.remove("d-none")),u.disabled=!1})),h(e,v.requiredText500),h(t,v.requiredText500),h(a,v.requiredText500),h(n,v.phone),h(i,v.email),h(s,v.requiredText70),h(o,v.requiredText70),h(r,v.username),h(l,v.password)},"register-requests":()=>new class{constructor(){this.successToast=new k,this._createDataTables(),this._createRegisterRequestsModal()}_createDataTables(){const e=[{data:"creationTime",name:"CreationTime",render:(e,t,a)=>new Date(e).toLocaleString(),searchable:!1},{data:"shortName",name:"ShortName",render:(e,t,a)=>o(e)},{data:"sender.name",name:"Sender.Name",render:(e,t,a)=>o(e)}],t=[{extend:"selectedSingle",text:"Просмотр",action:(e,t,a,n)=>{let i=t.row({selected:!0}).data();this.registerRequestModal.show(i)}}],a={tableId:"NewRegisterRequests",ajaxUrl:"/api/clinic/newRequests",serverSide:!0,advanced:{columns:e,buttons:t}};this.newRequestsDataTable=new C(a).getTable();const n=e.slice();n.push({data:"approved",name:"Approved",render:(e,t,a)=>e?"Одобрена":"Отклонена",searchable:!1});const i={tableId:"ProcessedRegisterRequests",ajaxUrl:"/api/clinic/processedRequests",serverSide:!0,advanced:{columns:n,buttons:t}};this.processedRequestsDataTable=new C(i).getTable()}_createRegisterRequestsModal(){const e={title:"Информация о заявке",readonly:!0,data:[{id:"date",path:"creationTime",label:"Дата",type:"input-text",render:e=>new Date(e).toLocaleString()},{id:"full-name",path:"fullName",label:"Полное наименование",type:"input-text"},{id:"short-name",path:"shortName",label:"Сокращенное наименование",type:"input-text"},{id:"address",path:"address",label:"Адрес",type:"input-text"},{id:"phone",path:"phone",label:"Телефон",type:"input-text"},{id:"email",path:"email",label:"Электронная почта",type:"input-text"},{id:"contact-person",path:"sender.name",label:"Контактное лицо",type:"input-text",render:e=>`${e.name}, ${e.position}`},{id:"username",path:"sender.userName",label:"Username",type:"input-text"}],buttons:[{text:"Одобрить",action:e=>this._manageRegisterRequest(e,!0),className:"btn btn-success",visibility:e=>!e.approved},{text:"Отклонить",action:e=>this._manageRegisterRequest(e,!1),className:"btn btn-danger",visibility:e=>!e.processed}]};this.registerRequestModal=new S(e)}async _manageRegisterRequest(e,t){const a={id:e.id,approved:t};await c("/api/clinic/manageRequest",a)&&(this.registerRequestModal.hide(),this.successToast.show(),this.newRequestsDataTable.ajax.reload(),this.processedRequestsDataTable.ajax.reload())}},"user-list":()=>function(){const e="True"==document.getElementById("UsersList").dataset.global,t=new k,a={id:"username",path:"username",type:"input-text",label:"Имя пользователя",validityCheck:v.username},n={id:"password",path:"password",type:"input-password",label:"Пароль",validityCheck:v.password},i=x('#custom-modal-1 input[data-custom-modal-id="password"]'),s=x('#custom-modal-3  input[data-custom-modal-id="password"]'),r={id:"name",path:"name",type:"input-text",label:"ФИО",validityCheck:v.requiredText70},l={id:"position",path:"position",type:"input-text",label:"Должность",validityCheck:v.requiredText70},d={title:"Создание нового пользователя",data:[a,n,i,r,l,_()],buttons:[{text:"Сохранить",action:async e=>await async function(e){const t={name:e.name,position:e.position,username:e.username,password:e.password,roleId:+e.role.id},a=await c("/api/user/create",t);I(u,a)}(e)}]},u=new S(d),p={title:e=>`Редактирование профиля пользователя ${e.username}`,data:[r,l,_()],buttons:[{text:"Сохранить",action:async e=>await async function(e){const t={name:e.name,position:e.position,roleId:+e.role.id},a=await c(`/api/user/update/${e.id}`,t);I(h,a)}(e)}]},h=new S(p),f=new S({title:e=>`Задать новый пароль для пользователя ${e.username}`,data:[n,s],buttons:[{text:"Сохранить",action:async e=>await async function(e){const t={password:e.password},a=await c(`/api/user/update/${e.id}`,t);I(f,a)}(e)}]}),y={tableId:"UsersList",ajaxUrl:"/api/user/list",serverSide:!0,scroll:!0,advanced:{columns:[{data:"id",name:"Id",searchable:!1,orderable:!0},{data:"name",name:"Name",render:(e,t,a)=>o(e),searchable:!0,orderable:!0},{data:"role.name",name:"Role.Name",render:(e,t,a)=>o(e),searchable:!0,orderable:!0}],buttons:[{text:"Создать",action:function(e,t,a,n){u.show(null)}},{extend:"selectedSingle",text:"Изменить профиль",action:function(e,t,a,n){let i=t.row({selected:!0}).data();h.show(i)}},{extend:"selectedSingle",text:"Изменить пароль",action:function(e,t,a,n){let i=t.row({selected:!0}).data();f.show(i)}}]}},b={data:"clinicShortName",name:"ClinicShortName",render:(e,t,a)=>o(e),searchable:!0,orderable:!0};e&&y.advanced.columns.push(b);const g=new C(y);function x(e){return{id:"password-confirmation",path:null,type:"input-password",label:"Повторите пароль",validityCheck:function(e){return new m("Пароли не совпадают",(t=>t.value!==document.querySelector(e).value))}(e)}}function w(){let t=[new Option("Сотрудник","3"),new Option("Модератор клиники","2"),new Option("Заблокированный","4")];return e&&t.push(new Option("Администратор сайта","1")),t}function _(){return{id:"roleId",path:"role.id",type:"select",label:"Тип аккаунта",options:w()}}function I(e,a){a?(e.hide(),t.show(),g.ajaxReload()):e.enableButtons()}}(),"user-login":()=>function(){const e="d-none",t=document.login,a=document.querySelector(".alert-danger"),n=document.getElementById("LoginButton");n.addEventListener("click",(async function(i){i.preventDefault(),n.disabled=!0,a.classList.add(e);const s=new FormData(t),o=Object.fromEntries(s),r=await c("/api/user/login",o);r&&(!0===r.succeed?location.replace("/"):a.classList.remove(e)),n.disabled=!1}))}()});document.addEventListener("DOMContentLoaded",(e=>A.load()))}},t={};function a(n){if(t[n])return t[n].exports;var i=t[n]={exports:{}};return e[n].call(i.exports,i,i.exports,a),i.exports}a.m=e,a.d=(e,t)=>{for(var n in t)a.o(t,n)&&!a.o(e,n)&&Object.defineProperty(e,n,{enumerable:!0,get:t[n]})},a.g=function(){if("object"==typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"==typeof window)return window}}(),a.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),a.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},(()=>{var e={179:0},t=[[789,736]],n=()=>{};function i(){for(var n,i=0;i<t.length;i++){for(var s=t[i],o=!0,r=1;r<s.length;r++){var l=s[r];0!==e[l]&&(o=!1)}o&&(t.splice(i--,1),n=a(a.s=s[0]))}return 0===t.length&&(a.x(),a.x=()=>{}),n}a.x=()=>{a.x=()=>{},o=o.slice();for(var e=0;e<o.length;e++)s(o[e]);return(n=i)()};var s=i=>{for(var s,o,[l,d,c,u]=i,m=0,p=[];m<l.length;m++)o=l[m],a.o(e,o)&&e[o]&&p.push(e[o][0]),e[o]=0;for(s in d)a.o(d,s)&&(a.m[s]=d[s]);for(c&&c(a),r(i);p.length;)p.shift()();return u&&t.push.apply(t,u),n()},o=self.webpackChunkProfOsmotr_Web=self.webpackChunkProfOsmotr_Web||[],r=o.push.bind(o);o.push=s})(),a.x()})();