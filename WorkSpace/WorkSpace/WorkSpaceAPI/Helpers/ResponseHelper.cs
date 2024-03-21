using Common.Constants;
using Entities.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace WorkSpaceAPI.Helpers
{
    public class ResponseHelper
    {
        public static IActionResult CreateResourceResponse(object? data, string message = MessageConstants.GlobalCreated)
        {
            ApiResponse response = new()
            {
                StatusCode = StatusCodes.Status201Created,
                Message = message,
                Data = data,
                Success = true
            };
            return new ObjectResult(response);
        }

        public static IActionResult SuccessResponse(object? data,string message = MessageConstants.GlobalSuccess)
        {
            ApiResponse response = new()
            {
                StatusCode= StatusCodes.Status200OK,
                Message = message,
                Data= data,
                Success= true
            };
            return new ObjectResult(response);
        }

        public static IActionResult ErrorResponse(object? data, string message = MessageConstants.GlobalError)
        {
            ApiResponse response = new()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Data = data,
                Success = true
            };
            return new ObjectResult(response);
        }
    }
}
