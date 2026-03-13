using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;

namespace demoWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("File is empty.");
            }
                                                                             
            var result = await fileRepository.UploadAsync(request);

            var response = new FileUploadResponseDto
            {
                Id = result.Id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{result.FileName}",
                CreatedDate = result.CreatedDate,
                StateId = result.StateId 
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetallFile([FromQuery] paginationDto pagination)
        {
            var files = await fileRepository.GetAll(pagination.PageNumber, pagination.PageSize);
            var totalRecord = await fileRepository.getTotalCount();

            var response = new List<FileUploadResponseDto>();
            foreach (var file in files)
            {
                response.Add(new FileUploadResponseDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FileExtension = file.FileExtension,
                    FileSize = file.FileSize,
                    FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{file.FileName}",
                    CreatedDate = file.CreatedDate

                });
            }
            return Ok(new
            {
                totalRecord,
                pageNumber = pagination.PageNumber,
                pageSize = pagination.PageSize,
                data = response
            });

        }
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult>getfileByid(Guid id)
        {
            var ExistFile = await fileRepository.GetById(id);
            if(ExistFile == null)
            {
                return NotFound();
            }
            var response = new FileUploadResponseDto
            {
                Id = ExistFile.Id,
                FileName = ExistFile.FileName,
                FileExtension = ExistFile.FileExtension,
                FileSize = ExistFile.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{ExistFile.FileName}",
                CreatedDate = ExistFile.CreatedDate,
                StateId = ExistFile.StateId

            };
            return Ok(response);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> UpdateFile(Guid id , [FromForm] FileUploadDto request)
        {
            var result = await fileRepository.UpdateById(id, request);
            if (result == null)
            {
                return NotFound();
            }
            var response = new FileUploadResponseDto
            {
                Id = result.Id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{result.FileName}",
                CreatedDate = result.CreatedDate,
                StateId = result.StateId

            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult>DleteFile(Guid id)
        {
            var result = await fileRepository.DeleteFileByid(id);
            if(result == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", result.FileName);

            // Delete physical file
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var response = new FileUploadResponseDto
            {
                Id = result.Id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{result.FileName}",
                CreatedDate = result.CreatedDate

            };
            return Ok(response);

        }
        [HttpGet("by-state/{stateId}")]
        public async Task<IActionResult>getfileByState(Guid stateId)
        {
            var isFile_Exist = await fileRepository.getByStateId(stateId);
            if(isFile_Exist == null)
            {
                return NotFound();
            }
            var response = isFile_Exist.Select(file => new FileUploadResponseDto
            {
                Id = file.Id, 
                FileName = file.FileName,
                FileExtension = file.FileExtension,
                FileSize = file.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/{file.FileName}",
                CreatedDate = file.CreatedDate
            }).ToList();
            return Ok(response);

        }




    }
}

