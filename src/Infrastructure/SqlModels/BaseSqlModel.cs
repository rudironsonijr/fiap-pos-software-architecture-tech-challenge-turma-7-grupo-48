﻿namespace Infrastructure.SqlModels;

public class BaseSqlModel
{
	public int Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}