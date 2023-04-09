using API.Infrastucture.Errors;
using API.Infrastucture.Extensions;
using AutoMapper;
using Core.Services.Contracts;
using EFModels.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Account;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(ITokenService tokenService, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IMapper mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                UserName = user.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExists(registerDto.Email).Result.Value) 
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email is in use" } });

            var user = new AppUser { 
                Email = registerDto.Email,
                UserName= registerDto.Email,
                DisplayName = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var foundUser = await _userManager.FindByEmailAsync(user.Email);
            var userDto = _mapper.Map<UserDto>(foundUser);

            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email) =>
             await _userManager.FindByEmailAsync(email) != null;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetAccount()
        {
            var user = await User.Get(_userManager);

            var resultUser = _mapper.Map<UserDto>(user);

            resultUser.Token = _tokenService.CreateToken(user);

            return resultUser;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var user = await User.GetWithAddress(_userManager);

            return _mapper.Map<AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> PutAddress(AddressDto addressDto)
        {
            var user = await User.GetWithAddress(_userManager);

            user.Address = _mapper.Map<Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest("Problem updating the user");

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        [HttpGet("testauth")]
        [Authorize]
        public async Task<string> Test()
        {
            return "secret string";
        }
    }
}
