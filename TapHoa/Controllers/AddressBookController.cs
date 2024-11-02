using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

[Authorize]
public class AddressBookController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;

    public AddressBookController(ApplicationDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Create()
    {
        var provinces = await GetProvincesAsync();
        ViewBag.Provinces = provinces;
        return View();
    }

    [HttpPost]
    public IActionResult Create(SoDiaChi soDiaChi)
    {
        if (ModelState.IsValid)
        {
            soDiaChi.KhachhangId = User.FindFirst("Matk")?.Value;
            _context.SoDiaChis.Add(soDiaChi);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(soDiaChi);
    }

    private async Task<List<Province>> GetProvincesAsync()
    {
        var response = await _httpClient.GetAsync("https://example.com/api/vietnam/provinces");
        response.EnsureSuccessStatusCode();
        
        var jsonString = await response.Content.ReadAsStringAsync();
        var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonString);
        return provinces;
    }

    private async Task<List<District>> GetDistrictsAsync(string provinceId)
    {
        var response = await _httpClient.GetAsync($"https://example.com/api/vietnam/districts?provinceId={provinceId}");
        response.EnsureSuccessStatusCode();
        
        var jsonString = await response.Content.ReadAsStringAsync();
        var districts = JsonConvert.DeserializeObject<List<District>>(jsonString);
        return districts;
    }

    private async Task<List<Ward>> GetWardsAsync(string districtId)
    {
        var response = await _httpClient.GetAsync($"https://example.com/api/vietnam/wards?districtId={districtId}");
        response.EnsureSuccessStatusCode();
        
        var jsonString = await response.Content.ReadAsStringAsync();
        var wards = JsonConvert.DeserializeObject<List<Ward>>(jsonString);
        return wards;
    }
}

// Mẫu class cho Province, District và Ward
public class Province
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class District
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Ward
{
    public string Id { get; set; }
    public string Name { get; set; }
}
