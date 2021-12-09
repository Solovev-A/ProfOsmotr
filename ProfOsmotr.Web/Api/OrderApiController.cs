﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.BL;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Api
{
    delegate Task<ExaminationResultIndexResponse> SaveIndexFunc(int targetId, SaveExaminationResultIndexRequest request);

    [Route("api/order")]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderApiController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("addExamination")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> AddExamination([FromBody] AddOrderExaminationQuery resource)
        {
            var request = mapper.Map<SaveOrderExaminationRequest>(resource);

            var response = await orderService.AddExaminationAsync(request);

            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var examinationResource = mapper.Map<OrderExaminationDetailedResource>(response.Result);

            return Ok(examinationResource);
        }

        [HttpPost("addItem")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> AddItem([FromBody] AddOrderItemQuery resource)
        {
            var request = mapper.Map<AddOrderItemRequest>(resource);

            var response = await orderService.AddItemAsync(request);

            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var itemResource = mapper.Map<OrderItemDetailedResource>(response.Result);

            return Ok(itemResource);
        }

        [HttpPost("deleteItem")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> DeleteItem([FromBody] int id)
        {
            var response = await orderService.DeleteItemAsync(id);

            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            return Ok(new { succeed = true });
        }

        [HttpGet("getExaminations")]
        public async Task<IActionResult> GetExaminations()
        {
            var examinations = await orderService.GetExaminationsAsync();
            var examinationResources = mapper.Map<IEnumerable<OrderExaminationDetailedResource>>(examinations);

            var targetGroups = await orderService.GetTargetGroupsAsync();
            var targetGroupResuorces = mapper.Map<IEnumerable<TargetGroupResource>>(targetGroups);

            var result = new ExaminationsResource(examinationResources, targetGroupResuorces);

            return Ok(result);
        }

        [HttpGet("getExaminationsMin")]
        public async Task<IActionResult> GetExaminationsMin()
        {
            var examinations = await orderService.GetExaminationsShortDataAsync();
            var resource = mapper.Map<IEnumerable<OrderExaminationResource>>(examinations);

            return Ok(resource);
        }

        [HttpGet("getIndexes/{examinationId}")]
        public async Task<IActionResult> GetIndexes(int examinationId)
        {
            var response = await orderService.GetExaminationResultIndexes(examinationId);

            if (!response.Succeed)
                return BadRequest(response.Message);

            var resource = mapper.Map<IEnumerable<ExaminationResultIndexResource>>(response.Result);

            return Ok(resource);
        }

        [HttpPost("examination/{examinationId}/index")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> CreateIndex(int examinationId, [FromBody] SaveExaminationResultIndexQuery resource)
        {
            return await SaveExaminationIndex(examinationId, resource, orderService.AddIndexAsync);
        }

        [HttpPost("index/{id}")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> UpdateIndex(int id, [FromBody] SaveExaminationResultIndexQuery resource)
        {
            return await SaveExaminationIndex(id, resource, orderService.UpdateIndexAsync);
        }

        [HttpDelete("index/{id}")]
        [AuthorizeAdministrator]
        public async Task<IActionResult> DeleteIndex(int id)
        {
            var response = await orderService.DeleteIndexAsync(id);
            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));
            var result = mapper.Map<ExaminationResultIndexResource>(response.Result);
            return Ok(result);
        }

        [HttpGet("getOrder")]
        public async Task<IActionResult> GetOrder(bool nocache)
        {
            IEnumerable<DAL.OrderItem> order = await orderService.GetOrderAsync(nocache);
            var orderResource = mapper.Map<IEnumerable<OrderItemDetailedResource>>(order);

            return Ok(orderResource);
        }

        [HttpPost("updateExamination")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> UpdateExamination([FromBody] UpdateOrderExaminationQuery resource)
        {
            var request = mapper.Map<SaveOrderExaminationRequest>(resource);

            var response = await orderService.UpdateExaminationAsync(resource.Id, request);

            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var examinationResource = mapper.Map<OrderExaminationDetailedResource>(response.Result);

            return Ok(examinationResource);
        }

        [HttpPost("updateItem")]
        [ModelStateValidationFilter]
        [AuthorizeAdministrator]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateOrderItemQuery resource)
        {
            var request = mapper.Map<UpdateOrderItemRequest>(resource);

            var response = await orderService.UpdateItemAsync(request);

            if (!response.Succeed)
                return BadRequest(new ErrorResource(response.Message));

            var itemResource = mapper.Map<OrderItemDetailedResource>(response.Result);

            return Ok(itemResource);
        }

        private async Task<IActionResult> SaveExaminationIndex(int targetId, SaveExaminationResultIndexQuery resource, SaveIndexFunc func)
        {
            var request = mapper.Map<SaveExaminationResultIndexRequest>(resource);
            ExaminationResultIndexResponse response = await func(targetId, request);

            if (!response.Succeed)
                return BadRequest(response.Message);

            var result = mapper.Map<ExaminationResultIndexResource>(response.Result);
            return Ok(result);
        }
    }
}